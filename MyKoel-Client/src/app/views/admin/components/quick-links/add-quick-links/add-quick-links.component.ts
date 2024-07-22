import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import Swal from 'sweetalert2';
import { QuickLinksService } from '../../../services/quick-links.service';
@Component({
  selector: 'app-add-quick-links',
  templateUrl: './add-quick-links.component.html',
  styleUrl: './add-quick-links.component.scss'
})
export class AddQuickLinksComponent {

  isAddMode: Boolean = true;
  submitted = false;
  deletedAttachmentIds: number[] = [];
  private unsubscribe: Subscription = new Subscription();
  quickLinkform: FormGroup;
  id!: number;/////////////

  isImageFail: boolean;
  isImageSuccess: boolean;
  imageSrc: any;
  attachmentId: number=0;
  viewImage: any;
  imageUrl: any;
  path: any;
  isViewImage: boolean = true;
  filename: any;
  filetype: any;
  constructor(
    public quickLinksService: QuickLinksService,
    private sanitizer: DomSanitizer,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public datepipe: DatePipe,
    private router: Router,){

    }
    ngOnInit(): void {

      this.id = this.route.snapshot.params['id'];
      this.isAddMode = !this.id;
      this.initializeForm();
      if (!this.isAddMode) {
        this.getDetailsById(+this.id);
      }
    }

  getDetailsById(id = 0) {
    this.unsubscribe.add(
      this.quickLinksService
        .getMenuHierarchyDetail(id)
        .subscribe((x) => {
          console.log(x);
          this.quickLinkform.patchValue(x);
          this.ontoggle(x.isIcon,"icon")
          this.ontoggle(x.isImage,"image")
        })
    );
  }

  initializeForm() {
    this.quickLinkform = new FormGroup({

      mainMenuGroupId: new FormControl(0),
      menuGroupName: new FormControl(null, Validators.required),
      sequence: new FormControl('', Validators.required),
      icon: new FormControl('', ),
      route: new FormControl(null, ),
      imageIcon: new FormControl('', ),
      flag: new FormControl('Quick Links', Validators.required),
      imageSrc: new FormControl(null, ),
      isActive: new FormControl(false,),
      isChild: new FormControl(false,),
      isImage: new FormControl(false,),
      isRoute: new FormControl(true,),
      isPopup: new FormControl(false,),
      isIcon: new FormControl(null,),

    });
  }

  ///////////////////////////////////////////////////////////////////////
  openFile(url: string) {
    window.open(url, '_blank');
  }
  // Logo Upload Functionality
  url: any = null;
  onSelectFile(event: any) {
    if (event.target.files && event.target.files[0]) {
      console.log(event.target.files[0]);
      var extn = event.target.files[0].name.split('.').pop();
      var name = event.target.files[0].name;
      this.filename=name;
      this.filetype=extn;
      // if(event.target.files[0].size  < 50000 || event.target.files[0].size > 200000)
      if (event.target.files[0].size > 5000000) {
        console.log('test img');

        Swal.fire({
          icon: 'error',
          text: 'Image size must be less than 5MB!',
        });
      }
      if (event.target.files[0].size <= 5000000) {
        var reader = new FileReader();
        reader.readAsDataURL(event.target.files[0]);
        reader.onload = (event) => {
          this.imageSrc = event.target?.result;
          var img = new Image();
          this.url = event.target?.result as string;
          img.src = event.target?.result as string;
          this.quickLinkform.controls['imageSrc'].setValue(img.src);
          this.quickLinkform.controls['imageIcon'].setValue(img.src);
          this.isImageFail = false;
          this.isViewImage = false;
          this.viewImage = null;
          this.isImageSuccess = true;
          let element: HTMLElement = document.getElementById(
            'img'
          ) as HTMLElement;
          element.blur();
        };
      }
    } else {
      this.isImageFail = true;
      this.quickLinkform.value.imageIcon = '';
      this.url = null;
      event.target.value = null;
      event.target.result = null;
      this.isImageSuccess = false;
      this.isViewImage = true;
    }
  }
onToggleChange(e:any,type=""){
  this.ontoggle(e.target.checked,type)
}
  ontoggle(e:any,type=""){
    console.log(e);
if(e){
  if(type==='icon'){
     this.quickLinkform.controls['icon'].setValidators([Validators.required]);
    this.quickLinkform.controls['icon'].updateValueAndValidity();
  }

  if(type==='image'){
     this.quickLinkform.controls['imageIcon'].setValidators([Validators.required]);
    this.quickLinkform.controls['imageIcon'].updateValueAndValidity();
  }

}else{
  if(type==='icon'){
    this.quickLinkform.controls['icon'].clearValidators();
   this.quickLinkform.controls['icon'].updateValueAndValidity();
 }

 if(type==='image'){
    this.quickLinkform.controls['imageIcon'].clearValidators();
   this.quickLinkform.controls['imageIcon'].updateValueAndValidity();
 }

}

  }
  onSubmit() {
    this.submitted = true;

    if (this.quickLinkform.invalid) {
      return;
    }
    if (this.isAddMode) {
      this.addvacancy();
    } else {
      this.updatevacancy();
    }
  }

  addvacancy() {
    console.log(this.quickLinkform);

    var formDetails = this.quickLinkform.getRawValue();

    this.quickLinksService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.quickLinksService
        .addMenuHierarchy(formDetails)
        .subscribe(
          (res: any) => {
            if (res.status === 200) {
              this.quickLinksService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>Quick link added successfully !!!</span>',
                'success'
              );
              this.routeToList();
            } else if (res.status === 400) {
              this.quickLinksService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          },
          (err) => {
            this.quickLinksService.isLoadingSubject.next(false);
            Swal.fire(
              'Error!',
              '<span>Something went wrong, please try again later !!!</span>',
              'error'
            );
          }
        )
    );
  }

  updatevacancy() {
    var formDetails = this.quickLinkform.getRawValue();

    this.quickLinksService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.quickLinksService
        .UpdateMenuHierarchy(formDetails)
        .subscribe(
          (res: any) => {
            if (res.status === 200) {
                if (this.deletedAttachmentIds.length > 0) {
                  // this.DeleteAttacments(this.deletedAttachmentIds);
                } else {
                  this.routeToList();
                }
              Swal.fire(
                'Success!',
                '<span>Quick link updated successfully !!!</span>',
                'success'
              );
            } else if (res.status === 400) {
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }

            this.quickLinksService.isLoadingSubject.next(false);
          },
          (err) => {
            this.quickLinksService.isLoadingSubject.next(false);
            Swal.fire(
              'Error!',
              '<span>Something went wrong, please try again later !!!</span>',
              'error'
            );
          }
        )
    );
  }
  routeToList(){
    this.router.navigate(['/admin/quick-links']);
  }
  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
  }
}
