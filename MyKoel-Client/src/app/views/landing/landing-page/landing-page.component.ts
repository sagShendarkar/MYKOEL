import { LandingPageService } from './../services/landing-page.service';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { BehaviorSubject, Subscription } from 'rxjs';
import { HeaderService } from 'src/app/containers/admin-layout/services/header.service';
import { HolidayCalenderService } from '../../admin/services/holiday-calender.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss'
})
export class LandingPageComponent {
wallpaperImage:any="";
vision="";
mission="";
values="";
  isDisplayslider$=  new BehaviorSubject<boolean>(false);
  isBirthdayDisplayslider$=  new BehaviorSubject<boolean>(false);
  isBirthdayLoading$=  new BehaviorSubject<boolean>(false);
  isAnnouncementLoading$=  new BehaviorSubject<boolean>(false);
  isNewHiresLoading$=  new BehaviorSubject<boolean>(false);
  isVacancyLoading$=  new BehaviorSubject<boolean>(false);
  isNoBirthday$=  new BehaviorSubject<boolean>(false);
  userName:any="";
  private unsubscribe: Subscription = new Subscription();
  public visionModal = false;
  public misionModal = false;
  public valueModal = false;
  public canteenMenuModal = false;
  public holidayCalenderModal = false;
  imageSrc = [
    '../../../../assets/images/images.jpg'
  ];

  slides: any[] = [];
  constructor(
    private domSanitizer: DomSanitizer,public headerService:HeaderService,
    public landingPageService:LandingPageService,public holidayCalenderService: HolidayCalenderService,
  ) {

    this.userName=localStorage.getItem('username')!==null?localStorage.getItem('username')?.toString():"";

    headerService.isDisplayBreadcrumb$.next(false);



    // this.landingPageService.newsList$.next(this.slides[2] );
  }

ngOnInit(): void {
  this.getLocationList();
  this.getWallpaperMenus();
  this.getQuickLinksMenus();
  this.getAnnouncementList();
  this.getNewsList();
  this.getNewHiresList();
  this.getBirthdayList();
  this.getHolidayCalendarList();
  this.getVacancyList();
  this.getOurVisionList();
  this.getOurMissionList();
  this.getOurValuesList();
}
ngAfterViewInit(): void {

  this.wallpaperImage=(localStorage.getItem('WallpaperPath')!==null&&localStorage.getItem('WallpaperPath')!=='null')?localStorage.getItem('WallpaperPath')?.toString():"../../../../assets/images/banner.png";

}

getLocationList() {
  this.unsubscribe.add(
    this.holidayCalenderService.getLocationList().subscribe((res) => {
      this.holidayCalenderService.locationList$.next(res);
    })
  );
}
onLocationChange(event:any){
console.log(event);
if(event){

  this.getHolidayCalendarList(event.locations);
}else{

  this.getHolidayCalendarList();
}
}
getWallpaperMenus(){
  this.unsubscribe.add(
    this.landingPageService.getLandingPageMenus(1,'Wallpaper Menus').subscribe((res)=>{
console.log(res);
this.landingPageService.wallpaperMenus$.next(res);
    })
  );
}
getQuickLinksMenus(){
  this.unsubscribe.add(
    this.landingPageService.getLandingPageMenus(1,'Quick Links').subscribe((res)=>{

this.landingPageService.quickLinksMenus$.next(res);
    })
  );
}
getHolidayCalendarList(location=""){
  this.unsubscribe.add(
    this.landingPageService.getHolidayCalendarList(location).subscribe((res)=>{

this.landingPageService.holidayCalendarList$.next(res);
    })
  );
}
getVacancyList(){
  this.isVacancyLoading$.next(true);
  this.unsubscribe.add(
    this.landingPageService.getVacancyList().subscribe((res)=>{

this.landingPageService.vacancyList$.next(res);
this.isVacancyLoading$.next(false);
    })
  );
}
getAnnouncementList(){
  this.isAnnouncementLoading$.next(true);
  this.unsubscribe.add(
    this.landingPageService.getSectionList('Announcement',2).subscribe((res)=>{
console.log(res);
this.landingPageService.announcementList$.next(res);
this.isAnnouncementLoading$.next(false);
    })
  );
}
getOurVisionList(){
  this.unsubscribe.add(
    this.landingPageService.getSectionList('Our Vision',1).subscribe((res)=>{
console.log(res);
this.vision=res[0].description;
    })
  );
}
getOurMissionList(){
  this.unsubscribe.add(
    this.landingPageService.getSectionList('Our Mission',1).subscribe((res)=>{
console.log(res);
this.mission=res[0].description;
    })
  );
}
getOurValuesList(){
  this.unsubscribe.add(
    this.landingPageService.getSectionList('Our Values',1).subscribe((res)=>{
console.log(res);
this.values=res[0].description;
    })
  );
}
getNewsList(){
  this.unsubscribe.add(
    this.landingPageService.getSectionList('News',5).subscribe((res)=>{
console.log(res);
this.landingPageService.newsList$.next(res);
this.isDisplayslider$.next(true);

    })
  );
}

getBirthdayList(){
  this.isBirthdayLoading$.next(true);
  this.unsubscribe.add(
    this.landingPageService.getBirthdayList().subscribe((res)=>{
console.log(res);
if(res.length===0){
  this.isNoBirthday$.next(true);
}
this.landingPageService.birthdayList$.next(res);
this.isBirthdayLoading$.next(false);

this.isBirthdayDisplayslider$.next(true);
    })
  );
}
openFile(url: string) {
  window.open(url, '_blank');
}
getNewHiresList(){
  this.isNewHiresLoading$.next(true);
  this.unsubscribe.add(
    this.landingPageService.getSectionList('New Hires',5).subscribe((res)=>{
console.log(res);
this.landingPageService.newHiresList$.next(res);

  this.isNewHiresLoading$.next(false);
    })
  );
}
  onItemChange($event: any): void {
    // console.log('Carousel onItemChange', $event);
  }

  handleLiveDemoChange(event: boolean) {
    this.visionModal = event;
  }
  handleLiveDemoChange1(event: boolean) {
    this.misionModal = event;
  }
  handleLiveDemoChange2(event: boolean) {
    this.valueModal = event;
  }
  handleLiveDemoChange3(event: boolean) {
    this.canteenMenuModal = event;
  }
  handleLiveDemoChange4(event: boolean) {
    this.holidayCalenderModal = event;
  }

openModalPopup(event: boolean,popupName=''){
if(popupName==='Vision'){

    this.visionModal = event;
}
if(popupName==='Mision'){

  this.misionModal = event;
}
if(popupName==='Value'){

  this.valueModal = event;
}
if(popupName==='Canteen Menu'){

  this.canteenMenuModal = event;
}
if(popupName==='Holiday Calender'){

  this.holidayCalenderModal = event;
}
}
  ngOnDestroy(): void {
     this.headerService.isDisplayBreadcrumb$.next(true);

this.landingPageService.vacancyList$.next([]);
this.landingPageService.newHiresList$.next([]);
this.landingPageService.newsList$.next([]);
this.landingPageService.announcementList$.next([]);
this.landingPageService.birthdayList$.next([]);
  }
}
