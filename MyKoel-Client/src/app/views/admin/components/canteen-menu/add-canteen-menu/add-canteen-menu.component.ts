import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HolidayCalenderService } from '../../../services/holiday-calender.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { BreakfastService } from '../../../services/breakfast.service';
import { LunchService } from '../../../services/lunch.service';

@Component({
  selector: 'app-add-canteen-menu',
  templateUrl: './add-canteen-menu.component.html',
  styleUrl: './add-canteen-menu.component.scss'
})
export class AddCanteenMenuComponent {

  submitted = false;
  BreakfastList:any=[]
  selectedBreakfastList:any=[]
  lunchList:any=[]
  selectedlunchList:any=[]
  private unsubscribe: Subscription = new Subscription();
  canteenMenuform: FormGroup;
  constructor(private router: Router,
    public holidayCalenderService: HolidayCalenderService,
    public breakfastService: BreakfastService,
    public lunchService: LunchService,
  ){

  }
  ngOnInit(): void {

    this.getLocationList();
    this.getBreakfastDropdownList();
    this.getLunchDropdownList();
    this.initializeForm();
  }

  getLocationList() {
    this.unsubscribe.add(
      this.holidayCalenderService.getLocationList().subscribe((res) => {
        this.holidayCalenderService.locationList$.next(res);
      })
    );
  }
  getLunchDropdownList() {
    this.unsubscribe.add(
      this.lunchService.getLunchDropdownList().subscribe((res) => {
        this.lunchService.LunchDropdownList$.next(res);
      })
    );
  }
  getBreakfastDropdownList() {
    this.unsubscribe.add(
      this.breakfastService.getBreakfastDropdownList().subscribe((res) => {
        this.breakfastService.BreakfastDropdownList$.next(res);
        this.BreakfastList=res;
      })
    );
  }
  initializeForm() {
    this.canteenMenuform = new FormGroup({

      date: new FormControl(null, Validators.required),
      location: new FormControl(null, Validators.required),
    });
  }

  addToBreakfastList(breakfast:any){
this.selectedBreakfastList.push(breakfast);
this.BreakfastList.splice(this.BreakfastList.findIndex((a:any) => a.breakFastId === breakfast.breakFastId) , 1);
this.breakfastService.BreakfastDropdownList$.next(this.BreakfastList);
  }
  removeFromSelectedBreakfastList(breakfast:any){
this.BreakfastList.push(breakfast);
this.breakfastService.BreakfastDropdownList$.next(this.BreakfastList);

this.selectedBreakfastList.splice(this.selectedBreakfastList.findIndex((a:any) => a.breakFastId === breakfast.breakFastId) , 1);
  }



  addToLunchList(lunch:any){
    this.selectedlunchList.push(lunch);
    this.lunchList.splice(this.lunchList.findIndex((a:any) => a.lunchId === lunch.lunchId) , 1);
    this.lunchService.LunchDropdownList$.next(this.lunchList);
      }
      removeFromSelectedLunchList(lunch:any){
    this.lunchList.push(lunch);
    this.lunchService.LunchDropdownList$.next(this.lunchList);

    this.selectedlunchList.splice(this.selectedlunchList.findIndex((a:any) => a.lunchId === lunch.lunchId) , 1);
      }
  onSubmit(){}


  routeToList(){
    this.router.navigate(['/admin/vacancy-list']);
  }
  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
  }
}
