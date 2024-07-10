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

  public liveDemoVisible = false;
title=""
userName:any="";
btnDetails:any;
  private unsubscribe: Subscription = new Subscription();
  constructor(public headerService:HeaderService,private router: Router,    private route: ActivatedRoute,
    private authService: AuthService ) {


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
}
  toggleLiveDemo() {
    this.liveDemoVisible = !this.liveDemoVisible;
  }

  handleLiveDemoChange(event: boolean) {
    this.liveDemoVisible = event;
  }
  getMenuHierarchy(){
    this.unsubscribe.add(
      this.headerService.getMenuHierarchy(1).subscribe((res:any[])=>{
        this.headerService.menuList$.next(res);
        let menuList:any[]=[]
        let tempMenuList:any[]=res;
        console.log(res);
        tempMenuList.forEach((element:any) => {
if(menuList.length>0){

  if(element.menuGroupData.length>0){

  }else{

    menuList.push(element)
  }
}else{

if(element.menuGroupData.length>0){

}else{

  menuList.push(element)
}
}
});
console.log(menuList);

      })
    );
  }
  logout(){
    this.authService.logout()
  }
}
