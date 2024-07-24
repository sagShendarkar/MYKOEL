import { Subscription } from 'rxjs';
import { HeaderService } from './../services/header.service';
import { Component } from '@angular/core';
import { ActivatedRoute,    NavigationEnd, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-admin-header',
  standalone: false,
  templateUrl: './admin-header.component.html',
  styleUrl: './admin-header.component.scss'
})
export class AdminHeaderComponent {
selectedMenu=0
  public liveDemoVisible = false;
title=""
userName:any="";
ProfileImage:any="";
btnDetails:any;
  private unsubscribe: Subscription = new Subscription();
  constructor(public headerService:HeaderService,private router: Router,    private route: ActivatedRoute,
    private authService: AuthService ) {

      
  this.ProfileImage=(localStorage.getItem('ProfileImage')!==null&&localStorage.getItem('ProfileImage')!=='null')?localStorage.getItem('ProfileImage')?.toString():"./../../../../assets/images/profile.png";

      this.userName=localStorage.getItem('username')!==null?localStorage.getItem('username')?.toString():"";
  }
ngOnInit(): void {
  this.getMenuHierarchy();
 this.unsubscribe.add(
  this.headerService.addBtn$.subscribe(res=>{
    console.log(res);
    this.btnDetails=res
  })
 ) ;

 this.unsubscribe.add(
  this.headerService.isProfilechanged$.subscribe(res=>{
if(res===true){

  this.ProfileImage=(localStorage.getItem('ProfileImage')!==null&&localStorage.getItem('ProfileImage')!=='null')?localStorage.getItem('ProfileImage')?.toString():"./../../../../assets/images/profile.png";
  this.headerService.isProfilechanged$.next(false);
}
  })
 );
}

ngAfterViewInit(): void {

}
  toggleLiveDemo() {
    this.liveDemoVisible = !this.liveDemoVisible;
  }

  handleLiveDemoChange(event: boolean) {
    this.liveDemoVisible = event;
  }
  getMenuHierarchy(){
    this.unsubscribe.add(
      // this.headerService.getMenuHierarchy(1).subscribe((res:any[])=>{
      //   this.headerService.menuList$.next(res);

      // })
      this.headerService.get4LevelMenuHierarchy().subscribe((res:any[])=>{
        this.headerService.menuList$.next(res);

      })
    );
  }
  logout(){
    this.authService.logout();
  }

  getSelectedMenu(id=0){
    this.selectedMenu=id;
  }
}
