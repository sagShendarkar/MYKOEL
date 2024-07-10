import { environment } from './../../../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LandingPageService {
  //MenuHierarchy/HomePageMenuList

  wallpaperMenus$=  new BehaviorSubject<any[]>([]);
  quickLinksMenus$=  new BehaviorSubject<any[]>([]);
  footerLinksMenus$=  new BehaviorSubject<any[]>([]);
  baseUrl = environment.apiUrl1;
  constructor(private http:HttpClient,private router: Router) { }


  getLandingPageMenus(id:number=1,Flag:string='')
  {
    let params=new HttpParams();
    // params=params.append('UserId',id);
    params=params.append('Flag',Flag);
    return this.http.get<any>(this.baseUrl+'MenuHierarchy/ShowMenuList',{params});
  }
}
