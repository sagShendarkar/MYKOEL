import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { OrdinalDatePipe } from './customPipe/ordinal-date.pipe';
import { OrdinalRangeDatePipe } from './customPipe/ordinal-range-date.pipe';


@NgModule({
  declarations: [
    OrdinalDatePipe,
    OrdinalRangeDatePipe
  ],
  imports: [
    CommonModule,
    SharedRoutingModule
  ],
  exports:[OrdinalDatePipe,OrdinalRangeDatePipe]
})
export class SharedModule { }
