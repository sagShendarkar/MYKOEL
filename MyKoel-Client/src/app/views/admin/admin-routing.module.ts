import { AuthGuard } from './../../guards/auth.guard';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyAnnouncementComponent } from './components/company-announcement/company-announcement.component';
import { AddCompanyAnnouncementComponent } from './components/add-company-announcement/add-company-announcement.component';
import { VisionMissionValuesComponent } from './components/vision-mission-values/vision-mission-values.component';
import { ProfileSettingComponent } from './components/profile-setting/profile-setting.component';
import { ViewMoreAnnouncementComponent } from './components/view-more-announcement/view-more-announcement.component';
import { ViewAnnouncementInfoComponent } from './components/view-announcement-info/view-announcement-info.component';

const routes: Routes = [
  {
    path: 'company-announcement',
    component: CompanyAnnouncementComponent,
    canActivate:[AuthGuard],
    data: {
      title: 'Company Announcement',
      isDisplayAddBtn:true,
      addBtn:{
        title:"Add Announcement",
        url:"/admin/add-company-announcement"
      },
        backBtnUrl:"/home"
    }
  },
  {
    path: 'add-company-announcement',
    component: AddCompanyAnnouncementComponent,
    canActivate:[AuthGuard],
    data: {
      title: 'Add Announcement',
      isDisplayAddBtn:false,
        backBtnUrl:"/admin/company-announcement"
    }
  },
  {
    path: 'vision-mission-values',
    component: VisionMissionValuesComponent,
    canActivate:[AuthGuard],
    data: {
      title: 'Vision, Mission, Values',
      isDisplayAddBtn:false,
        backBtnUrl:"/home"
    }
  },
  {
    path: 'profile-setting',
    component: ProfileSettingComponent,
    canActivate:[AuthGuard],
    data: {
      title: 'Profile Setting',
      isDisplayAddBtn:false,
        backBtnUrl:"/home"
    }
  },
  {
    path: 'view-more-announcement',
    component: ViewMoreAnnouncementComponent,
    canActivate:[AuthGuard],
    data: {
      title: 'Company Announcement',
      isDisplayAddBtn:false,
        backBtnUrl:"/home"
    }
  },
  {
    path: 'view-announcement-info',
    component: ViewAnnouncementInfoComponent,
    canActivate:[AuthGuard],
    data: {
      title: 'Company Announcement info',
      isDisplayAddBtn:false,
        backBtnUrl:"/admin/view-more-announcement"
    }
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
