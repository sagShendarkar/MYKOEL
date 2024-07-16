import { Router } from '@angular/router';
import { GlobalConstants } from './../../../common/global-constants';
import { environment } from './../../../../environments/environment';
import { PaginationParams } from './../../../models/paginationParams';
import { PaginatedResult, Pagination } from './../../../models/pagination';
import { Injectable } from '@angular/core';
import { BehaviorSubject, finalize, map, Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SectionService {

  pagination:Pagination;
  paginationParams:PaginationParams;
  baseUrl = environment.apiUrl1;

  userId=localStorage.getItem('userId');
  imageUrl = environment.imageUrl+GlobalConstants.MACHINEIMGFOLDER;


  isLoading$: Observable<boolean>;
  isLoadingSubject: BehaviorSubject<boolean>;

  sectionTransactionList$=  new BehaviorSubject<any>([]);
  constructor(private http:HttpClient,private router: Router) {

    this.paginationParams=Object.assign({}, GlobalConstants.paginationDefParams);
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
  }

  getSectionTransactionList<T>(paginationParams:PaginationParams):Observable<PaginatedResult<T>>{

    let params=this.getPaginationHeader(paginationParams.pageNumber,paginationParams.pageSize);
    params=params.append('searchPagination',paginationParams.searchPagination);
    params=params.append('Flag',paginationParams.flag);

    // params=params.append('UserId',this.userId!==null?(+this.userId):0);
    return this.getPaginatedResult(this.baseUrl + 'SectionTransaction/ShowSectionList',params);
  }

  private getPaginatedResult<T>(url='',params:any):Observable<PaginatedResult<T>>  {
    this.isLoadingSubject.next(true);
    const paginatedResult:PaginatedResult<T>=new PaginatedResult<T>();
     return this.http.get<T>(url, { observe: 'response', params }).pipe(
       map(response => {
         return this.paginationTest<T>(paginatedResult, response);
       }),
        finalize(()=>this.isLoadingSubject.next(false))
     );
   }
  private paginationTest<T>(paginatedResult: PaginatedResult<T>, response:any) {
    paginatedResult.result = response.body;
    if (response.headers.get('Pagination') !== null) {
      paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
    }
    return paginatedResult;
  }

  private getPaginationHeader(pageNumber:number,pageSize:number){
    let params=new HttpParams();
    params=params.append('pageNumber',pageNumber.toString());
    params=params.append('pageSize',pageSize.toString());
    return params;
  }


  DeleteSectionTransaction(id:number){
    return this.http.post(this.baseUrl+'SectionTransaction/DeleteSection/'+id, {});
  }

  getSectionDetail(id:number)
  {
    return this.http.get<any>(this.baseUrl+'SectionTransaction/GetSectionTrnById/'+id);
  }
  addSection(SectionValue:any=null)
  {
    return this.http.post(this.baseUrl + 'SectionTransaction/AddSection',SectionValue);

  }
  UpdateSection(SectionValue:any=null)
  {
    return this.http.post(this.baseUrl + 'SectionTransaction/UpdateSection',SectionValue);

  }

  addUpdateSection(SectionValue:any=null)
  {
    return this.http.post(this.baseUrl + 'SectionTransaction/UpdateCKEditorData',SectionValue);

  }
  getVisionMissionValueDetails(flag='')
  {
    let params=new HttpParams();
    params=params.append('Flag',flag);

    return this.http.get<any>(this.baseUrl+'SectionTransaction/ShowSectionList',{params});
  }
}
