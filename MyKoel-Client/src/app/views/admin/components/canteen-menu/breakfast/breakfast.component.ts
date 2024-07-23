import { BreakfastService } from './../../../services/breakfast.service';
import { ExcelService } from './../../../../../services/common/excel.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import { PaginationParams } from 'src/app/models/paginationParams';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-breakfast',
  templateUrl: './breakfast.component.html',
  styleUrl: './breakfast.component.scss'
})
export class BREAKFASTComponent {
  pagination: any;
  pageIndex = 0;
  private unsubscribe: Subscription = new Subscription();
  paginationParams: PaginationParams;
  isNoData: boolean = false;

  isAddMode: Boolean = true;
  searchUpdate = new Subject<string>();

  submitted = false;
  BreakfastForm: FormGroup;
  constructor(
    private router: Router,
    public breakfastService: BreakfastService,
    public excelService:ExcelService) {}
    ngOnInit(): void {

      // this.getLocationList();
      this.initializeForm();
      this.paginationParams =
        this.breakfastService.paginationParams;
      this.paginationParams.pageNumber = 1;
      // this.paginationParams.location = "Khadki";
      this.loadBreakfastList();

      this.searchUpdate.pipe(
        debounceTime(500),
        distinctUntilChanged())
        .subscribe(value => {
          console.log(value);
          this.paginationParams.searchPagination=value;
          this.loadBreakfastList();
        });
    }

    initializeForm() {
      this.BreakfastForm = new FormGroup({

        breakFastId: new FormControl(0, ),
        breakFastName: new FormControl(null, Validators.required),
        isActive: new FormControl(true, Validators.required),
      });
    }
    loadBreakfastList() {
      this.unsubscribe.add(
        this.breakfastService
          .getBreakfastList(this.paginationParams)
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
              this.breakfastService.BreakfastList$.next(
                res.result
              );
              this.pagination = res.pagination;
            },
            (err) => {
              this.breakfastService.isLoadingSubject.next(false);
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
      this.breakfastService.paginationParams.pageNumber =
        event.page;
      this.loadBreakfastList();
    }

    patchValue(breakfastDetails:any){
      this.BreakfastForm.patchValue(breakfastDetails);
      this.isAddMode=false;
    }
    onSubmit(){
      this.submitted = true;
      // stop here if form is invalid
  console.log(this.BreakfastForm);

      if (this.BreakfastForm.invalid) {
        return;
      }
      if(this.isAddMode){

      this.addBreakFast();
      }else{

      this.updateBreakFast();
      }
    }

  addBreakFast() {


    this.breakfastService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.breakfastService
        .addBreakfast(this.BreakfastForm.value)
        .subscribe(
          (res: any) => {
            this.submitted=false;
            this.resetForm();
            if (res.status === 200) {
              this.breakfastService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>BreakFast added successfully !!!</span>',
                'success'
              );
             this.loadBreakfastList()
            } else if (res.status === 400) {
              this.breakfastService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          },
          (err) => {
            this.breakfastService.isLoadingSubject.next(false);
            Swal.fire(
              'Error!',
              '<span>Something went wrong, please try again later !!!</span>',
              'error'
            );
          }
        )
    );
  }
  updateBreakFast() {


    this.breakfastService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.breakfastService
        .updateBreakfast(this.BreakfastForm.value)
        .subscribe(
          (res: any) => {
            this.isAddMode=true;
            this.submitted=false;
            this.resetForm();
            if (res.status === 200) {
              this.breakfastService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>BreakFast updated successfully !!!</span>',
                'success'
              );
             this.loadBreakfastList()
            } else if (res.status === 400) {
              this.breakfastService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          },
          (err) => {
            this.breakfastService.isLoadingSubject.next(false);
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
    this.BreakfastForm.controls['breakFastId'].setValue(0);
    this.BreakfastForm.controls['breakFastName'].setValue(null);
    this.BreakfastForm.controls['isActive'].setValue(true);

  }
    ngOnDestroy(): void {
      this.unsubscribe.unsubscribe();
    }

}
