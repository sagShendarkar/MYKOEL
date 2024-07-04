import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, finalize, map } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnumDDService {

  baseUrl = environment.apiUrl1;


  isLoading$: Observable<boolean>;
  isLoadingSubject: BehaviorSubject<boolean>;

  equipmentTypeDD$=  new BehaviorSubject<any>([]);
  statusDD$=  new BehaviorSubject<any>([]);
  investmentTypeDD$=  new BehaviorSubject<any>([]);
  projectImpactDD$=  new BehaviorSubject<any>([]);
  assetConditionDD$=  new BehaviorSubject<any>([]);
  machineCategoryDD$=  new BehaviorSubject<any>([]);
  assetDisposleDD$=  new BehaviorSubject<any>([]);
 ppfTypeDD$=  new BehaviorSubject<any>([]);
  constructor(private http:HttpClient,private router: Router) {

    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
  }


  getInvestmentTypeDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/InvestmentTypeDD');
  }

  getProjectImpactDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/ProjectImpactDD');
  }
  getEquipmentTypeDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/EquipmentTypeDD');
  }
  getStatusDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/StatusDD');
  }

  getMachineCategoryDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/MachineCategoryDD');
  }

  getAssetConditionDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/AssetConditionDD');
  }

  getAssetDisposleDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/AssetDisposleDD');
  }

  getPPFTypeDD()
  {
    return this.http.get<any>(this.baseUrl+'EnumDropDown/PPFTypeDD');
  }
}
