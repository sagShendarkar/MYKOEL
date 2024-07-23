import { LunchService } from './../../../services/lunch.service';
import { ExcelService } from './../../../../../services/common/excel.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import { PaginationParams } from 'src/app/models/paginationParams';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-lunch',
  templateUrl: './lunch.component.html',
  styleUrl: './lunch.component.scss'
})
export class LUNCHComponent {
  pagination: any;
  pageIndex = 0;
  private unsubscribe: Subscription = new Subscription();
  paginationParams: PaginationParams;
  isNoData: boolean = false;

  isAddMode: Boolean = true;
  searchUpdate = new Subject<string>();

  submitted = false;
  LunchForm: FormGroup;
  constructor(
    private router: Router,
    public lunchService: LunchService,
    public excelService:ExcelService) {}
    ngOnInit(): void {

      // this.getLocationList();
      this.initializeForm();
      this.paginationParams =
        this.lunchService.paginationParams;
      this.paginationParams.pageNumber = 1;
      // this.paginationParams.location = "Khadki";
      this.loadLunchList();

      this.searchUpdate.pipe(
        debounceTime(500),
        distinctUntilChanged())
        .subscribe(value => {
          console.log(value);
          this.paginationParams.searchPagination=value;
          this.loadLunchList();
        });
    }

    initializeForm() {
      this.LunchForm = new FormGroup({

        lunchId: new FormControl(0, ),
        lunchName: new FormControl(null, Validators.required),
        isActive: new FormControl(true, Validators.required),
      });
    }
    loadLunchList() {
      this.unsubscribe.add(
        this.lunchService
          .getLunchList(this.paginationParams)
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
              this.lunchService.LunchList$.next(
                res.result
              );
              this.pagination = res.pagination;
            },
            (err) => {
              this.lunchService.isLoadingSubject.next(false);
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
      this.lunchService.paginationParams.pageNumber =
        event.page;
      this.loadLunchList();
    }

    patchValue(lunchDetails:any){
      this.LunchForm.patchValue(lunchDetails);
      this.isAddMode=false;
    }
    onSubmit(){
      this.submitted = true;
      // stop here if form is invalid
  console.log(this.LunchForm);

      if (this.LunchForm.invalid) {
        return;
      }
      if(this.isAddMode){

      this.addLunch();
      }else{

      this.updateLunch();
      }
    }

  addLunch() {


    this.lunchService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.lunchService
        .addLunch(this.LunchForm.value)
        .subscribe(
          (res: any) => {
            this.submitted=false;
            this.resetForm();
            if (res.status === 200) {
              this.lunchService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>Lunch added successfully !!!</span>',
                'success'
              );
             this.loadLunchList()
            } else if (res.status === 400) {
              this.lunchService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          },
          (err) => {
            this.lunchService.isLoadingSubject.next(false);
            Swal.fire(
              'Error!',
              '<span>Something went wrong, please try again later !!!</span>',
              'error'
            );
          }
        )
    );
  }
  updateLunch() {


    this.lunchService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.lunchService
        .updateLunch(this.LunchForm.value)
        .subscribe(
          (res: any) => {
            this.isAddMode=true;
            this.submitted=false;
            this.resetForm();
            if (res.status === 200) {
              this.lunchService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>Lunch updated successfully !!!</span>',
                'success'
              );
             this.loadLunchList()
            } else if (res.status === 400) {
              this.lunchService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          },
          (err) => {
            this.lunchService.isLoadingSubject.next(false);
            Swal.fire(
              'Error!',
              '<span>Something went wrong, please try again later !!!</span>',
              'error'
            );
          }
        )
    );
  }

  resetForm(){
    this.LunchForm.controls['lunchId'].setValue(0);
    this.LunchForm.controls['lunchName'].setValue(null);
    this.LunchForm.controls['isActive'].setValue(true);

  }
    ngOnDestroy(): void {
      this.unsubscribe.unsubscribe();
    }

}
