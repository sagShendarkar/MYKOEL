import { LandingPageService } from './../../../views/landing/services/landing-page.service';
import { Component } from '@angular/core';
import { FooterComponent } from '@coreui/angular';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-admin-footer',
  standalone: false,
  templateUrl: './admin-footer.component.html',
  styleUrl: './admin-footer.component.scss'
})
export class AdminFooterComponent extends FooterComponent{
  private unsubscribe: Subscription = new Subscription();
  constructor(public landingPageService:LandingPageService) {
    super();
  }
ngOnInit(): void {
  //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
  //Add 'implements OnInit' to the class.
  this.getFooterMenus();
}

getFooterMenus(){
  this.unsubscribe.add(
    this.landingPageService.getLandingPageMenus(1,'Footer Menus').subscribe((res)=>{
console.log(res);  
this.landingPageService.footerLinksMenus$.next(res);
    })
  );
}
}
