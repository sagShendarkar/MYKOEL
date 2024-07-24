import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
// import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
// import {
//   ClassicEditor,
//   Bold,
//   Essentials,
//   Heading,
//   Indent,
//   IndentBlock,
//   Italic,
//   Link,
//   List,
//   MediaEmbed,
//   Paragraph,
//   Table,
//   Undo,Underline
// } from 'ckeditor5';

// import 'ckeditor5/ckeditor5.css';
import { Subscription } from 'rxjs';
import { SectionService } from '../../services/section.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-vision-mission-values',
  templateUrl: './vision-mission-values.component.html',
  styleUrl: './vision-mission-values.component.scss'
})
export class VisionMissionValuesComponent {
  title = 'angular';
  // public Editor = ClassicEditor;
  // public config = {
  //   toolbar: [
  //     'undo', 'redo', '|',
  //     'heading', '|', 'bold','Underline', 'italic', '|',
  //     'link', 'insertTable', 'mediaEmbed', '|',
  //     'bulletedList', 'numberedList', 'indent', 'outdent'
  //   ],
  //   plugins: [
  //     Bold,Underline,
  //     Essentials,
  //     Heading,
  //     Indent,
  //     IndentBlock,
  //     Italic,
  //     Link,
  //     List,
  //     MediaEmbed,
  //     Paragraph,
  //     Table,
  //     Undo
  //   ]
  // }


  submitted = false;
  private unsubscribe: Subscription = new Subscription();
  visionform: FormGroup;
  missionform: FormGroup;
  valuesform: FormGroup;
  constructor(public sectionService:SectionService){

  }
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.

    this.initializeForm();
    this.getVisionDetails();
    this.getMissionDetails();
    this.getValueDetails();
  }

  initializeForm() {
    this.visionform = new FormGroup({
      sectionid: new FormControl(0),
      sequence: new FormControl('0'),
      description: new FormControl(null, Validators.required),
       startdate: new FormControl(new Date(), Validators.required),
      enddate: new FormControl(new Date(), Validators.required),
      flag: new FormControl('Our Vision', Validators.required),
      title: new FormControl('Our Vision', Validators.required),
      attachment: new FormControl(null),
      isactive: new FormControl(true),
      isHtml: new FormControl(true),
      isAdd: new FormControl(true),

    });
    this.missionform = new FormGroup({
      sectionid: new FormControl(0),
      sequence: new FormControl('0'),
      description: new FormControl(null, Validators.required),
       startdate: new FormControl(new Date(), Validators.required),
      enddate: new FormControl(new Date(), Validators.required),
      flag: new FormControl('Our Mission', Validators.required),
      title: new FormControl('Our Mission', Validators.required),
      attachment: new FormControl(null),
      isactive: new FormControl(true),
      isHtml: new FormControl(true),
      isAdd: new FormControl(true),
    });
    this.valuesform = new FormGroup({
      sectionid: new FormControl(0),
      sequence: new FormControl('0'),
      description: new FormControl(null, Validators.required),
       startdate: new FormControl(new Date(), Validators.required),
      enddate: new FormControl(new Date(), Validators.required),
      flag: new FormControl('Our Value', Validators.required),
      title: new FormControl('Our Value', Validators.required),
      attachment: new FormControl(null),
      isHtml: new FormControl(true),
      isactive: new FormControl(true),
      isAdd: new FormControl(true),
    });
  }
getVisionDetails(){
  this.unsubscribe.add(
    this.sectionService.getVisionMissionValueDetails("Our Vision").subscribe(res=>{
if(res.length>0){
  this.visionform.controls['sectionid'].setValue(res[0].sectionid);
  this.visionform.controls['description'].setValue(res[0].description);
  this.visionform.controls['flag'].setValue(res[0].flag);
  this.visionform.controls['isAdd'].setValue(false);
}else{

  this.visionform.controls['isAdd'].setValue(true);
}
    })
  )
}
getMissionDetails(){
  this.unsubscribe.add(
    this.sectionService.getVisionMissionValueDetails("Our Mission").subscribe(res=>{
      if(res.length>0){
        this.missionform.controls['sectionid'].setValue(res[0].sectionid);
        this.missionform.controls['description'].setValue(res[0].description);
        this.missionform.controls['flag'].setValue(res[0].flag);
        this.missionform.controls['isAdd'].setValue(false);
      }else{

        this.missionform.controls['isAdd'].setValue(true);
      }
    })
  )
}
getValueDetails(){
  this.unsubscribe.add(
    this.sectionService.getVisionMissionValueDetails("Our Value").subscribe(res=>{
      if(res.length>0){
        this.valuesform.controls['sectionid'].setValue(res[0].sectionid);
        this.valuesform.controls['description'].setValue(res[0].description);
        this.valuesform.controls['flag'].setValue(res[0].flag);
        this.valuesform.controls['isAdd'].setValue(false);
      }else{

        this.valuesform.controls['isAdd'].setValue(true);
      }
    })
  )
}
onSubmit(){
  let array=[]
if(this.visionform.invalid===false){
  var formDetails = this.visionform.getRawValue();
  if(formDetails.description!==null){
    array.push(formDetails)
  }
}
if(this.missionform.invalid===false){
  var formDetails1 = this.missionform.getRawValue();
  if(formDetails1.description!==null){
    array.push(formDetails1)
  }
}
if(this.valuesform.invalid===false){
  var formDetails2 = this.valuesform.getRawValue();
  if(formDetails2.description!==null){
    array.push(formDetails2)
  }
}
if(array.length>0){

this.addUpdateSection(array);
}
}


addUpdateSection(formDetails:any) {
  console.log(this.visionform);


  this.sectionService.isLoadingSubject.next(true);
  this.unsubscribe.add(
    this.sectionService
      .addUpdateSection(formDetails)
      .subscribe(
        (res: any) => {
          if (res[0].status === 200) {
            this.sectionService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>  added successfully !!!</span>',
                'success'
              );


          } else if (res.status === 400) {
            this.sectionService.isLoadingSubject.next(false);
            Swal.fire(
              'Error!',
              '<span>Something went wrong, please try again later !!!</span>',
              'error'
            );
          }
        },
        (err) => {
          this.sectionService.isLoadingSubject.next(false);
          Swal.fire(
            'Error!',
            '<span>Something went wrong, please try again later !!!</span>',
            'error'
          );
        }
      )
  );
}

}
