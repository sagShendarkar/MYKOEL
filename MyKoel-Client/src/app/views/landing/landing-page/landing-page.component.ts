import { LandingPageService } from './../services/landing-page.service';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { BehaviorSubject, Subscription } from 'rxjs';
import { HeaderService } from 'src/app/containers/admin-layout/services/header.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss'
})
export class LandingPageComponent {
wallpaperImage:any="";

  isDisplayslider$=  new BehaviorSubject<boolean>(false);
  userName:any="";
  private unsubscribe: Subscription = new Subscription();
  public visionModal = false;
  public misionModal = false;
  public valueModal = false;
  public canteenMenuModal = false;
  imageSrc = [
    '../../../../assets/images/images.jpg'
  ];

  slidesLight = [
    'data:image/svg+xml;charset=UTF-8,%3Csvg%20width%3D%22800%22%20height%3D%22400%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20viewBox%3D%220%200%20800%20400%22%20preserveAspectRatio%3D%22none%22%3E%3Cdefs%3E%3Cstyle%20type%3D%22text%2Fcss%22%3E%23holder_1607923e7e2%20text%20%7B%20fill%3A%23AAA%3Bfont-weight%3Anormal%3Bfont-family%3AHelvetica%2C%20monospace%3Bfont-size%3A40pt%20%7D%20%3C%2Fstyle%3E%3C%2Fdefs%3E%3Cg%20id%3D%22holder_1607923e7e2%22%3E%3Crect%20width%3D%22800%22%20height%3D%22400%22%20fill%3D%22%23F5F5F5%22%3E%3C%2Frect%3E%3Cg%3E%3Ctext%20x%3D%22285.9296875%22%20y%3D%22217.75625%22%3EFirst%20slide%3C%2Ftext%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E',
    'data:image/svg+xml;charset=UTF-8,%3Csvg%20width%3D%22800%22%20height%3D%22400%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20viewBox%3D%220%200%20800%20400%22%20preserveAspectRatio%3D%22none%22%3E%3Cdefs%3E%3Cstyle%20type%3D%22text%2Fcss%22%3E%23holder_15ba800aa20%20text%20%7B%20fill%3A%23BBB%3Bfont-weight%3Anormal%3Bfont-family%3AHelvetica%2C%20monospace%3Bfont-size%3A40pt%20%7D%20%3C%2Fstyle%3E%3C%2Fdefs%3E%3Cg%20id%3D%22holder_15ba800aa20%22%3E%3Crect%20width%3D%22800%22%20height%3D%22400%22%20fill%3D%22%23EEE%22%3E%3C%2Frect%3E%3Cg%3E%3Ctext%20x%3D%22247.3203125%22%20y%3D%22218.3%22%3ESecond%20slide%3C%2Ftext%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E',
    'data:image/svg+xml;charset=UTF-8,%3Csvg%20width%3D%22800%22%20height%3D%22400%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20viewBox%3D%220%200%20800%20400%22%20preserveAspectRatio%3D%22none%22%3E%3Cdefs%3E%3Cstyle%20type%3D%22text%2Fcss%22%3E%23holder_15ba800aa21%20text%20%7B%20fill%3A%23999%3Bfont-weight%3Anormal%3Bfont-family%3AHelvetica%2C%20monospace%3Bfont-size%3A40pt%20%7D%20%3C%2Fstyle%3E%3C%2Fdefs%3E%3Cg%20id%3D%22holder_15ba800aa21%22%3E%3Crect%20width%3D%22800%22%20height%3D%22400%22%20fill%3D%22%23E5E5E5%22%3E%3C%2Frect%3E%3Cg%3E%3Ctext%20x%3D%22277%22%20y%3D%22218.3%22%3EThird%20slide%3C%2Ftext%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E'
  ];
  slides: any[] = [];
  constructor(
    private domSanitizer: DomSanitizer,public headerService:HeaderService,public landingPageService:LandingPageService
  ) {

    this.userName=localStorage.getItem('username')!==null?localStorage.getItem('username')?.toString():"";

    headerService.isDisplayBreadcrumb$.next(false);


    this.slides[2] = [
      {
        id: 0,
         title: 'First slide',
        subtitle: 'Nulla vitae elit libero, a pharetra augue mollis interdum.'
      },
      {
        id: 1,
        title: 'Second slide',
        subtitle: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.'
      },
      {
        id: 2,
         title: 'Third slide',
        subtitle: 'Praesent commodo cursus magna, vel scelerisque nisl consectetur.'
      }
    ];

    // this.landingPageService.newsList$.next(this.slides[2] );
  }

ngOnInit(): void {
  this.getWallpaperMenus();
  this.getQuickLinksMenus();
  this.getAnnouncementList();
  this.getNewsList();
  this.getNewHiresList();

}
ngAfterViewInit(): void {

  this.wallpaperImage=localStorage.getItem('WallpaperPath')!==null?localStorage.getItem('WallpaperPath')?.toString():"../../../../assets/images/banner.png";

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
getAnnouncementList(){
  this.unsubscribe.add(
    this.landingPageService.getSectionList('Announcement',2).subscribe((res)=>{
console.log(res);
this.landingPageService.announcementList$.next(res);
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
getNewHiresList(){
  this.unsubscribe.add(
    this.landingPageService.getSectionList('New Hires',5).subscribe((res)=>{
console.log(res);
this.landingPageService.newHiresList$.next(res);

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
}
  ngOnDestroy(): void {
     this.headerService.isDisplayBreadcrumb$.next(true);
  }
}
