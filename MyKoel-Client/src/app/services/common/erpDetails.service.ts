import { environment } from './../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, finalize, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ERPDetailsService {

  baseUrl = environment.apiUrl1;


  isLoading$: Observable<boolean>;
  isLoadingSubject: BehaviorSubject<boolean>;

  x1CodeDropDown$=  new BehaviorSubject<any>([]);
  ccCodeDropDown$=  new BehaviorSubject<any>([]);
  approvalHierarchy$=  new BehaviorSubject<any>([]);

  constructor(private http:HttpClient,private router: Router) {
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
  }


  AutoGenerateNumber(Form:number)
  {
    let params=new HttpParams();
    params=params.append('Form',Form);
    return this.http.get<any>(this.baseUrl+'ERPDetails/AutoGenerateNumber',{params});
  }
  getUserDetails()
  {
    let params=new HttpParams();
    let userId=localStorage.getItem('userId')
    params=params.append('UserId',userId!==null?(+userId):0);
    return this.http.get<any>(this.baseUrl+'ERPDetails/UserDetails',{params});
  }
  getAssetDetails(assetNumber:any)
  {
    let params=new HttpParams();
    params=params.append('AssetNumber',assetNumber);
    return this.http.get<any>(this.baseUrl+'ERPDetails/AssetDetails',{params});
  }
  getAssetATFDetails(assetNumber:any)
  {
    let params=new HttpParams();
    params=params.append('AssetNo',assetNumber);
    return this.http.get<any>(this.baseUrl+'ERPDetails/AssetATFDetails',{params});
  }

  getX1CodeDropDown()
  {
    return this.http.get<any>(this.baseUrl+'ERPDetails/X1CodeDropDown');
  }
  getCCCodeDropDown()
  {
    return this.http.get<any>(this.baseUrl+'ERPDetails/CCCodeDropDown');
  }
}
