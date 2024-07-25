import { CanteenMenuService } from './../../../services/canteen-menu.service';
import { ExcelService } from './../../../../../services/common/excel.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import { PaginationParams } from 'src/app/models/paginationParams';
import Swal from 'sweetalert2';
import { HolidayCalenderService } from '../../../services/holiday-calender.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-canteen-menu',
  templateUrl: './canteen-menu.component.html',
  styleUrl: './canteen-menu.component.scss'
})
export class CanteenMenuComponent {
  pagination: any;
  pageIndex = 0;
  private unsubscribe: Subscription = new Subscription();
  paginationParams: PaginationParams;
  isNoData: boolean = false;
  selectedLocation='';
  selectedDate='';
  isAddMode: Boolean = true;
  searchUpdate = new Subject<string>();
  submitted = false;
  CanteenMenuForm: FormGroup;
  constructor(
    private router: Router,
    public datepipe: DatePipe,
    public canteenMenuService: CanteenMenuService,
    public excelService:ExcelService,public holidayCalenderService: HolidayCalenderService) {}
    ngOnInit(): void {

      this.getLocationList();
      this.initializeForm();
      this.paginationParams =
        this.canteenMenuService.paginationParams;
      this.paginationParams.pageNumber = 1;
      // this.paginationParams.location = "Khadki";
      // this.paginationParams.date = "2024-07-11";
      //  this.loadCanteenMenuList();

      this.searchUpdate.pipe(
        debounceTime(500),
        distinctUntilChanged())
        .subscribe(value => {
          console.log(value);
          this.paginationParams.searchPagination=value;
          this.loadCanteenMenuList();
        });
    }

    getLocationList() {
      this.unsubscribe.add(
        this.holidayCalenderService.getLocationList().subscribe((res) => {
          this.holidayCalenderService.locationList$.next(res);
          if(res.length>0){

            let date=new Date();
            let Tdate = this.datepipe.transform(date,'yyyy-MM-dd');
      this.paginationParams.date = Tdate!==null? Tdate.toString():'';
      let sDate=this.datepipe.transform(date,'dd MMM yyyy');
      this.selectedDate= sDate!==null? sDate.toString():'';
            this.paginationParams.location = res[1].locations;
            this.selectedLocation = res[1].locations;
            this.loadCanteenMenuList();
          }
        })
      );
    }
    initializeForm() {
      this.CanteenMenuForm = new FormGroup({

        CanteenMenuId: new FormControl(0, ),
        CanteenMenuName: new FormControl(null, Validators.required),
        isActive: new FormControl(true, Validators.required),
      });
    }
    onLocationChange(event:any){
      this.paginationParams.location = event.locations;
      this.selectedLocation=event.locations;
      let date= this.datepipe.transform(this.selectedDate,'yyyy-MM-dd')
      this.paginationParams.date =date!==null?date:''  ;
      this.loadCanteenMenuList();
    }
    onDateChange(event:any){
console.log(event);
if(event){
  let date = this.datepipe.transform(event,'yyyy-MM-dd');
  this.paginationParams.date =date!==null?date:''  ;
  this.paginationParams.location = this.selectedLocation;
  this.loadCanteenMenuList();

}
    }
    loadCanteenMenuList() {
      this.unsubscribe.add(
        this.canteenMenuService
          .getCanteenMenusList(this.paginationParams)
          .subscribe(
            (res: any) => {
              console.log(res);
              let value =
                this.paginationParams.pageNumber * this.paginationParams.pageSize;
              this.pageIndex = value - this.paginationParams.pageSize;
              let data = res.result;
              // if (data.length === 0) {
              //   this.isNoData = true;
              // } else {
              //   this.isNoData = false;
              // }
              this.canteenMenuService.CanteenMenusList$.next(
                res.result
              );
              // this.pagination = res.pagination;
            },
            (err) => {
              this.canteenMenuService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          )
      );
    }
    pageChanged(event: any) {
      this.canteenMenuService.paginationParams.pageNumber =
        event.page;
      this.loadCanteenMenuList();
    }

    ngOnDestroy(): void {
      this.unsubscribe.unsubscribe();
    }

}
