import { environment } from './../../../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
addBtnDetails={
  title:"",
  url:""
}
  baseUrl = environment.apiUrl1;
  headerTitle$=  new BehaviorSubject<string>('');
  backBtnUrl$=  new BehaviorSubject<string>('');
  isDisplayBreadcrumb$=  new BehaviorSubject<boolean>(true);
  addBtn$=  new BehaviorSubject<any>(this.addBtnDetails);
  menuList$=  new BehaviorSubject<any[]>([]);
  isDisplayAddBtn$=  new BehaviorSubject<boolean>(false);
  constructor(private http:HttpClient,private router: Router) { }



  getMenuHierarchy(id:number=1)
  {
    let params=new HttpParams();
    // params=params.append('UserId',id);
    params=params.append('Flag',"Top MenuBar");
    return this.http.get<any>(this.baseUrl+'MenuHierarchy/ShowMenuList',{params});
  }
}
