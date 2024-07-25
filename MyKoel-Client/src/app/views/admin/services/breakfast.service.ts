import { GlobalConstants } from './../../../common/global-constants';
import { PaginationParams } from './../../../models/paginationParams';
import { PaginatedResult, Pagination } from './../../../models/pagination';
import { Router } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, finalize, map, Observable } from 'rxjs';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BreakfastService {


  pagination:Pagination;
  paginationParams:PaginationParams;
  baseUrl = environment.apiUrl1;
  isLoading$: Observable<boolean>;
  isLoadingSubject: BehaviorSubject<boolean>;
  BreakfastList$=  new BehaviorSubject<any>([]);
  BreakfastDropdownList$=  new BehaviorSubject<any>([]);
   locationList$=  new BehaviorSubject<any>([]);
  constructor(private http:HttpClient,private router: Router) {

    this.paginationParams=Object.assign({}, GlobalConstants.paginationDefParams);
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
   }

   getBreakfastList<T>(paginationParams:PaginationParams):Observable<PaginatedResult<T>>{

    let params=this.getPaginationHeader(paginationParams.pageNumber,paginationParams.pageSize);
    params=params.append('searchPagination',paginationParams.searchPagination);
    params=params.append('Location',paginationParams.location);

    return this.getPaginatedResult(this.baseUrl + 'Breakfast/ShowBreakFastList',params);
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



//uploadBreakfast

addBreakfast(formValue:any=null)
{
  return this.http.post(this.baseUrl + 'BreakFast/AddBreakfast',formValue);

}
getBreakfastDropdownList()
{
  return this.http.get(this.baseUrl + 'BreakFast/BreakfastDropdownList');

}
updateBreakfast(formValue:any=null)
{
  return this.http.post(this.baseUrl + 'BreakFast/UpdateBreakfastDetails',formValue);

}
}
