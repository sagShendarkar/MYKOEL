import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyPageComponent } from './my-page/my-page.component';
import { AuthGuard } from 'src/app/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: MyPageComponent,
    canActivate:[AuthGuard],
    data: {
      title: `My Page`,
      isDisplayAddBtn:false,
        backBtnUrl:"/home"
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MypageRoutingModule { }
