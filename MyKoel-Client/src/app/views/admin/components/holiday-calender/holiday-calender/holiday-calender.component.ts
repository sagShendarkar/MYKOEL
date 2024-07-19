import { ExcelService } from './../../../../../services/common/excel.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HolidayCalenderService } from './../../../services/holiday-calender.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import { PaginationParams } from 'src/app/models/paginationParams';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-holiday-calender',
  templateUrl: './holiday-calender.component.html',
  styleUrl: './holiday-calender.component.scss'
})
export class HolidayCalenderComponent {
  pagination: any;
  pageIndex = 0;
  private unsubscribe: Subscription = new Subscription();
  paginationParams: PaginationParams;
  isNoData: boolean = false;

  searchUpdate = new Subject<string>();
  /////////////////////////////

  statusList =
     {
      title: "Inward List", excelName: "Inward",
       excelHeader: ['TelematicsID', 'ChassisNumber', 'OEM', 'AssetType', 'BatteryNumber', 'BatteryType', 'BMSType'],

    }




///////////////////////////////
  file: File ; // Variable to store file to Upload

    fd = new FormData();
  submitted = false;
  holidayform: FormGroup;
  constructor(
    private router: Router,
    public holidayCalenderService: HolidayCalenderService,
    public excelService:ExcelService) {}

  ngOnInit(): void {

    this.getLocationList();
    this.initializeForm();
    this.paginationParams =
      this.holidayCalenderService.paginationParams;
    this.paginationParams.pageNumber = 1;
    // this.paginationParams.location = "Khadki";
    this.loadHolidayCalenderList();

    this.searchUpdate.pipe(
      debounceTime(500),
      distinctUntilChanged())
      .subscribe(value => {
        console.log(value);
        this.paginationParams.searchPagination=value;
        this.loadHolidayCalenderList();
      });
  }

  initializeForm() {
    this.holidayform = new FormGroup({

      Excelfile: new FormControl(null, Validators.required),
      Location: new FormControl(null, Validators.required),
    });
  }
  loadHolidayCalenderList() {
    this.unsubscribe.add(
      this.holidayCalenderService
        .getHolidayCalenderList(this.paginationParams)
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
            this.holidayCalenderService.HolidayCalenderList$.next(
              res.result
            );
            this.pagination = res.pagination;
          },
          (err) => {
            this.holidayCalenderService.isLoadingSubject.next(false);
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
    this.holidayCalenderService.paginationParams.pageNumber =
      event.page;
    this.loadHolidayCalenderList();
  }

  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
  }


  getLocationList() {
    this.unsubscribe.add(
      this.holidayCalenderService.getLocationList().subscribe((res) => {
        this.holidayCalenderService.locationList$.next(res);
      })
    );
  }
  url: any = null;

  onChange(event: any) {
    this.file = event.target.files[0];
  }
  onSubmit(){
    this.submitted = true;
    // stop here if form is invalid
console.log(this.holidayform);

    if (this.holidayform.invalid) {
      return;
    }
    this.uploadHolidayCalender();
  }


  uploadHolidayCalender() {
    console.log(this.holidayform);

    var formDetails = this.holidayform.getRawValue();
    let fd = new FormData();
    fd.append('Excelfile', this.file);

    fd.append('Location', formDetails.Location);
    // fd.append('Excelfile', this.file!==null?this.file:'');

    this.holidayCalenderService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.holidayCalenderService
        .uploadHolidayCalender(fd)
        .subscribe(
          (res: any) => {
            if (res.status === 200) {
              this.holidayCalenderService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>Announcement added successfully !!!</span>',
                'success'
              );
              this.submitted=false;
              this.holidayform.reset();
              this.loadHolidayCalenderList();
            } else if (res.status === 400) {
              this.holidayCalenderService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          },
          (err) => {
            this.holidayCalenderService.isLoadingSubject.next(false);
            Swal.fire(
              'Error!',
              '<span>Something went wrong, please try again later !!!</span>',
              'error'
            );
          }
        )
    );
  }
  downloadTemplate(){

    // this.excelService.generateExcel();
  }

  downloadExcel() {
    // this.commonServicesService.exportEmptyExcelTemplate(this.statusList.excelName + "Template", this.statusList.excelHeader);
  }
}
