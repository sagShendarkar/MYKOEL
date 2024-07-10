import { DatePipe } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-mood-check',
  templateUrl: './mood-check.component.html',
  styleUrl: './mood-check.component.scss'
})
export class MoodCheckComponent {

  moodTodayForm!: FormGroup;
  submitted = false;
moodList=[
  {
    moodName:"Very Sad",
    moodImg:"../../../../assets/images/very-sad.png",
  },
  {
    moodName:"Sad",
    moodImg:"../../../../assets/images/sad.png",
  },
  {
    moodName:"Good",
    moodImg:"../../../../assets/images/good.png",
  },
  {
    moodName:"Happy",
    moodImg:"../../../../assets/images/happy.png",
  },
  {
    moodName:"Very happy",
    moodImg:"../../../../assets/images/very-happy.png",
  },
];
selectedMoodIndex:any=null;
  public liveDemoVisible = false;
  @ViewChild('search') search: ElementRef;
  id!: number;
  constructor(
    private route: ActivatedRoute,public authService: AuthService,public datePipe: DatePipe, private router: Router) {
  }
ngOnInit(): void {

  this.id = this.route.snapshot.params['id'];
  this.initializeForm();

}
initializeForm() {
  this.moodTodayForm = new FormGroup({
    moodId: new FormControl(0),
    rating: new FormControl('', Validators.required),
    comment: new FormControl('', Validators.required),
    reportedDateTime: new FormControl(null ),
    userId: new FormControl(+this.id, Validators.required),
  });

}
ngAfterViewInit(): void {
  //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
  //Add 'implements AfterViewInit' to the class.

  this.search.nativeElement.click();
  this.liveDemoVisible=true;
}
  toggleLiveDemo() {
    this.liveDemoVisible = !this.liveDemoVisible;
  }

  handleLiveDemoChange(event: boolean) {
    this.liveDemoVisible = event;
  }
  onImgClick(i:any,mood:any){
this.selectedMoodIndex=i;
this.moodTodayForm.controls['rating'].setValue(mood.moodName);
console.log(this.moodTodayForm);

  }
  reasonText(e:any){

this.moodTodayForm.controls['comment'].setValue(e.target.value!==''?e.target.value:null);
  }

  moodToday() {
    var date = new Date();
    var transformDate = this.datePipe.transform(date, 'yyyy-MM-ddTHH:mm:ss');

    this.moodTodayForm.controls['reportedDateTime'].setValue(transformDate);

    this.submitted = true;
    // stop here if form is invalid
    if (this.moodTodayForm.invalid) {
      return;
    }

    this.authService.moodToday(this.moodTodayForm.value).subscribe((res: any) => {

      this.router.navigateByUrl("/home");
    })
  }
}
