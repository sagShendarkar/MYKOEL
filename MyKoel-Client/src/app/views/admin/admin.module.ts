import { BREAKFASTComponent } from './components/canteen-menu/breakfast/breakfast.component';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonGroupModule, ButtonModule, CardModule, DropdownModule, FormModule, GridModule, ListGroupModule,  SpinnerModule, TableModule } from '@coreui/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


// import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import {  BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { SharedModule } from '../../shared/shared.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AdminRoutingModule } from './admin-routing.module';
import { CompanyAnnouncementComponent } from './components/company-announcement/company-announcement.component';
import { AddCompanyAnnouncementComponent } from './components/add-company-announcement/add-company-announcement.component';
import { VisionMissionValuesComponent } from './components/vision-mission-values/vision-mission-values.component';
import { ProfileSettingComponent } from './components/profile-setting/profile-setting.component';
import { ViewMoreAnnouncementComponent } from './components/view-more-announcement/view-more-announcement.component';
import { ViewAnnouncementInfoComponent } from './components/view-announcement-info/view-announcement-info.component';
import { NewsViewMoreComponent } from './components/news-view-more/news-view-more.component';
import { ViewDetailedNewsComponent } from './components/view-detailed-news/view-detailed-news.component';
import { VacancyPostingComponent } from './components/vacancy-posting/vacancy-posting.component';
import { NewHiresComponent } from './components/new-hires/new-hires/new-hires.component';
import { AddNewHiresComponent } from './components/new-hires/add-new-hires/add-new-hires.component';
import { CompanyNewsComponent } from './components/company-news/company-news/company-news.component';
import { AddCompanyNewsComponent } from './components/company-news/add-company-news/add-company-news.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { HolidayCalenderComponent } from './components/holiday-calender/holiday-calender/holiday-calender.component';
import { AddVacancyComponent } from './components/vacancy/add-vacancy/add-vacancy.component';
import { VacancyListComponent } from './components/vacancy/vacancy-list/vacancy-list.component';
import { QuickLinksListComponent } from './components/quick-links/quick-links-list/quick-links-list.component';
import { AddQuickLinksComponent } from './components/quick-links/add-quick-links/add-quick-links.component';
import { CanteenMenuComponent } from './components/canteen-menu/canteen-menu/canteen-menu.component';
import { LUNCHComponent } from './components/canteen-menu/lunch/lunch.component';


import { AngularEditorModule } from '@kolkov/angular-editor';
@NgModule({
  declarations: [
    CompanyAnnouncementComponent,
    AddCompanyAnnouncementComponent,
    VisionMissionValuesComponent,
    ProfileSettingComponent,
    ViewMoreAnnouncementComponent,
    ViewAnnouncementInfoComponent,
    NewsViewMoreComponent,
    ViewDetailedNewsComponent,
    VacancyPostingComponent,
    NewHiresComponent,
    AddNewHiresComponent,
    CompanyNewsComponent,
    AddCompanyNewsComponent,
    HolidayCalenderComponent,
    AddVacancyComponent,
    VacancyListComponent,
    QuickLinksListComponent,
    AddQuickLinksComponent,
    CanteenMenuComponent,
    LUNCHComponent, BREAKFASTComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    PaginationModule,
    CommonModule,
    CardModule,
    FormModule,
    GridModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
    FormModule,
    ButtonModule,
    ButtonGroupModule,
    DropdownModule,
    SharedModule,
    ListGroupModule,
    SpinnerModule,
    TableModule,
    BsDatepickerModule,  NgSelectModule,
    AngularEditorModule

  ]
})
export class AdminModule { }
