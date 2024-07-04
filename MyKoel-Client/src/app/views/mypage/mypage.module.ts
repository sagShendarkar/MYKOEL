import { MypageRoutingModule } from './mypage-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyPageComponent } from './my-page/my-page.component';



@NgModule({
  declarations: [
    MyPageComponent
  ],
  imports: [
    CommonModule,
    MypageRoutingModule
  ]
})
export class MypageModule { }
