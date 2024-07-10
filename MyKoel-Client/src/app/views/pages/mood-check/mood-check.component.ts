import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-mood-check',
  templateUrl: './mood-check.component.html',
  styleUrl: './mood-check.component.scss'
})
export class MoodCheckComponent {
moodList=[
  {
    moodId:"",
    moodName:"Very Sad",
    moodImg:"../../../../assets/images/very-sad.png",
  },
  {
    moodId:"",
    moodName:"Sad",
    moodImg:"../../../../assets/images/sad.png",
  },
  {
    moodId:"",
    moodName:"Good",
    moodImg:"../../../../assets/images/good.png",
  },
  {
    moodId:"",
    moodName:"Happy",
    moodImg:"../../../../assets/images/happy.png",
  },
  {
    moodId:"",
    moodName:"Very happy",
    moodImg:"../../../../assets/images/very-happy.png",
  },
];
selectedMoodIndex:any=null;
  public liveDemoVisible = false;
  @ViewChild('search') search: ElementRef;
  constructor() {
  }
ngOnInit(): void {

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
  onImgClick(i:any){
this.selectedMoodIndex=i;
  }
}
