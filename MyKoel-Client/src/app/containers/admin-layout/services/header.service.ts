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
  isProfilechanged$=  new BehaviorSubject<boolean>(false);
  isDisplayBreadcrumb$=  new BehaviorSubject<boolean>(true);
  addBtn$=  new BehaviorSubject<any>(this.addBtnDetails);
  menuList$=  new BehaviorSubject<any[]>([]);
  isDisplayAddBtn$=  new BehaviorSubject<boolean>(false);
  constructor(private http:HttpClient,private router: Router) { }



  getMenuHierarchy(id:number=1)
  {
    let params=new HttpParams();
    // params=params.append('UserId',id);
    let Grade=localStorage.getItem('Grade');
    if(Grade==="SysAdmin"){
    params=params.append('Grade',"SysAdmin");
    }

    params=params.append('Flag',"Top MenuBar");

    return this.http.get<any>(this.baseUrl+'MenuHierarchy/ShowMenuList',{params});
  }


  get4LevelMenuHierarchy(userId=0,level:number=0,menusId=0)
  {
    let params=new HttpParams();
    // params=params.append('UserId',userId);
    // let Grade=localStorage.getItem('Grade');
    // if(Grade==="SysAdmin"){
    // params=params.append('Grade',"SysAdmin");
    // }

    // if(menusId!==0){
    // params=params.append('MenuId',menusId);
    // }

    // params=params.append('Flag','Top MenuBar');

    return this.http.get<any>(this.baseUrl+'MenuHierarchy/4thLevelMenuList');
  }
}
