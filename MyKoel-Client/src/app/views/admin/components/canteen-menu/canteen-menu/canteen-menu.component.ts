import { CanteenMenuService } from './../../../services/canteen-menu.service';
import { ExcelService } from './../../../../../services/common/excel.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import { PaginationParams } from 'src/app/models/paginationParams';
import Swal from 'sweetalert2';
import { HolidayCalenderService } from '../../../services/holiday-calender.service';

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

  isAddMode: Boolean = true;
  searchUpdate = new Subject<string>();

  submitted = false;
  CanteenMenuForm: FormGroup;
  constructor(
    private router: Router,
    public canteenMenuService: CanteenMenuService,
    public excelService:ExcelService,public holidayCalenderService: HolidayCalenderService) {}
    ngOnInit(): void {

      this.getLocationList();
      this.initializeForm();
      this.paginationParams =
        this.canteenMenuService.paginationParams;
      this.paginationParams.pageNumber = 1;
      // this.paginationParams.location = "Khadki";
     // this.loadCanteenMenuList();

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
              if (data.length === 0) {
                this.isNoData = true;
              } else {
                this.isNoData = false;
              }
              this.canteenMenuService.CanteenMenusList$.next(
                res.result
              );
              this.pagination = res.pagination;
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
