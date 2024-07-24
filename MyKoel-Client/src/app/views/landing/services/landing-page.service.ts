import { DatePipe } from '@angular/common';
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
  holidayCalendarList$=  new BehaviorSubject<any[]>([]);
  vacancyList$=  new BehaviorSubject<any[]>([]);
  newsList$=  new BehaviorSubject<any[]>([]);
  newHiresList$=  new BehaviorSubject<any[]>([]);
 birthdayList$=  new BehaviorSubject<any[]>([]);
  announcementList$=  new BehaviorSubject<any[]>([]);
  baseUrl = environment.apiUrl1;
  constructor(private http:HttpClient,private router: Router,private datePipe: DatePipe) { }


  getLandingPageMenus(id:number=1,Flag:string='')
  {
    let params=new HttpParams();
    let Grade=localStorage.getItem('Grade');
    if(Grade==="SysAdmin"){
    params=params.append('Grade',"SysAdmin");
    }
    params=params.append('Flag',Flag);
    return this.http.get<any>(this.baseUrl+'MenuHierarchy/4thLevelMenuList',{params});
  }
  getSectionList(Flag:string='',PageSize=0,PageNumber=1)
  {
    let params=new HttpParams();
    params=params.append('Flag',Flag);
    params=params.append('PageSize',PageSize);
    params=params.append('PageNumber',PageNumber);
    return this.http.get<any>(this.baseUrl+'SectionTransaction/ShowSectionList',{params});
  }
  getHolidayCalendarList(Location:string='')
  {
    let params=new HttpParams();
    params=params.append('Location',Location);
    return this.http.get<any>(this.baseUrl+'HolidayCalender/HolidayCalendarList',{params});
  }
  getVacancyList(Location:string='')
  {
    let params=new HttpParams();
    // params=params.append('Location',Location);
    return this.http.get<any>(this.baseUrl+'VacancyPosting/ShowVacancyList',{params});
  }
  getBirthdayList()
  {
   let myDate = new Date();
  let today = this.datePipe.transform(myDate, 'yyyy-MM-dd');
    let params=new HttpParams();
      params=params.append('Date',today!==null?today:"");
    return this.http.get<any>(this.baseUrl+'Wallpaper/GetBirthdayList',{params});
  }
}
