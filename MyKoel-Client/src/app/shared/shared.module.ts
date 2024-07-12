import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { OrdinalDatePipe } from './customPipe/ordinal-date.pipe';
import { OrdinalRangeDatePipe } from './customPipe/ordinal-range-date.pipe';
import { TruncateWordsPipe } from './customPipe/truncate-words.pipe';


@NgModule({
  declarations: [
    OrdinalDatePipe,
    OrdinalRangeDatePipe,
    TruncateWordsPipe
  ],
  imports: [
    CommonModule,
    SharedRoutingModule
  ],
  exports:[OrdinalDatePipe,OrdinalRangeDatePipe,TruncateWordsPipe]
})
export class SharedModule { }
