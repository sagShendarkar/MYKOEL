import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonGroupModule, ButtonModule, CardModule, DropdownModule, FormModule, GridModule, ListGroupModule,  SpinnerModule, TableModule } from '@coreui/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

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
    BsDatepickerModule
  ]
})
export class AdminModule { }
