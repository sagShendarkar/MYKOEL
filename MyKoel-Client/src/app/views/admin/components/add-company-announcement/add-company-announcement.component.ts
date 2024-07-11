import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SectionService } from '../../services/section.service';
import { Subscription } from 'rxjs';
import Swal from 'sweetalert2';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-add-company-announcement',
  templateUrl: './add-company-announcement.component.html',
  styleUrl: './add-company-announcement.component.scss'
})
export class AddCompanyAnnouncementComponent {

  isAddMode: Boolean = true;
  submitted = false;
  deletedAttachmentIds: number[] = [];
  private unsubscribe: Subscription = new Subscription();
  announcementform: FormGroup;
  id!: number;
  /////////////

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
  constructor(public sectionService: SectionService,

    private sanitizer: DomSanitizer,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public datepipe: DatePipe,
    private router: Router,){}
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.

    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.initializeForm();

    if (!this.isAddMode) {
      // this.formName = 'Edit';
      this.getDetailsById(+this.id);
    }
  }


  getDetailsById(id = 0) {
    this.unsubscribe.add(
      this.sectionService
        .getSectionDetail(id)
        .subscribe((x) => {
          console.log(x);
          x.enddate = this.datepipe.transform(x.enddate,'dd MMM yyyy');
          x.startdate = this.datepipe.transform(x.startdate,'dd MMM yyyy');
          this.announcementform.patchValue(x);
          if (x.attachment.length > 0) {
            this.loadAttachments(x.attachment);
          }


        })
    );
  }

  loadAttachments(attachments: any) {
    attachments.forEach((element: any) => {
if(element.imageflag===2){

      const add = this.announcementform.get('attachment') as FormArray;
      add.push(
        this.fb.group({
          attachmentid: [element.attachmentid],
          sectionid: [this.id],
          path: [element.path],
          filename: [element.filename],
          filetype: [element.filetype],
          title: [element.title],
          imagesrc: [element.imagesrc],
          ispopup: [false],
          isredirected: [false],
          src: [''],
          image: [element.image],
          isactive: [true],
          filepath: [element.image, Validators.required],
          imagePath: [element.path, Validators.required],
          isImageSuccess: [false],
          imageflag: [2],
        })
      );
}
if(element.imageflag===1){

  this.announcementform.controls['imagePath'].setValue(element.image);
  this.imageSrc=element.image;
  this.attachmentId=element.attachmentid;
  this.filename=element.filename;
  this.filetype=element.filetype;
  this.path=element.path;
}
    });
  }
  initializeForm() {
    this.announcementform = new FormGroup({

      sectionid: new FormControl(0),
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      isimage: new FormControl(true, Validators.required),
      startdate: new FormControl('', Validators.required),
      enddate: new FormControl('', Validators.required),
      flag: new FormControl('Announcement', Validators.required),
      sequence: new FormControl('', ),
      isactive: new FormControl(true),
      category: new FormControl('', Validators.required),

      imagePath: new FormControl(null, Validators.required),
      imageSrc: new FormControl(null),
      attachment: this.fb.array([]),
    });
  }


  get attachment(): FormArray {
    return this.announcementform.get('attachment') as FormArray;
  }

  addNew() {
    const add = this.announcementform.get('attachment') as FormArray;
    add.push(
      this.fb.group({
        attachmentid: [0],
        sectionid: [0],
        path: [null],
        filename: [''],
        filetype: [''],
        title: [''],
        imagesrc: [''],
        ispopup: [false],
        isredirected: [false],
        src: [''],
        image: [''],
        isactive: [true],
        filepath: ['', Validators.required],
        imagePath: ['', Validators.required],
        isImageSuccess: [false],
        imageflag: [2],
      })
    );
    console.log(this.announcementform.getRawValue());
  }

  deleteA(index: number) {
    const add = this.announcementform.get('attachment') as FormArray;
    add.at(index);
    let values = add.at(index).getRawValue();
    console.log(values.attachmentId);
    if (values.attachmentId > 0) {
      this.deletedAttachmentIds.push(values.attachmentId);
    }
    console.log(this.deletedAttachmentIds);

    add.removeAt(index);
  }


  // Logo Upload Functionality
  url1: any = null;
  onSelectAttachmentsFile(event: any, index = 0) {
    if (event.target.files && event.target.files[0]) {
      console.log(event.target.files[0]);
      var extn = event.target.files[0].name.split('.').pop();
      var name = event.target.files[0].name;

      console.log(extn);
      // if(event.target.files[0].size  < 50000 || event.target.files[0].size > 200000)
      if (event.target.files[0].size > 5000000) {
        console.log('test img');

        Swal.fire({
          icon: 'error',
          text: 'File size must be less than 5MB!',
        });
      }
      if (event.target.files[0].size <= 5000000) {
        var reader = new FileReader();
        let imagesUpload = this.sanitizer.bypassSecurityTrustUrl(
          URL.createObjectURL(event.target.files[0])
        );

        reader.readAsDataURL(event.target.files[0]);
        reader.onload = (event) => {
          // this.imageSrc = event.target?.result;
          var img = new Image();
          this.url1 = event.target?.result as string;
          img.src = event.target?.result as string;
          this.attachment.at(index).get('imagesrc')?.setValue(img.src);
          this.attachment.at(index).get('filepath')?.setValue(img.src);
          this.attachment.at(index).get('imagePath')?.setValue(imagesUpload);
          this.attachment.at(index).get('filetype')?.setValue(extn);
          this.attachment.at(index).get('title')?.setValue(name);
          this.attachment.at(index).get('filename')?.setValue(name);

          this.attachment.at(index).get('isImageSuccess')?.setValue(true);
          this.attachment.at(index).get('isViewImage')?.setValue(true);
          this.attachment.at(index).get('isImageFail')?.setValue(false);
          // this.companyForm.controls['imageSrc'].setValue(img.src);
          // this.companyForm.controls['imagePath'].setValue(img.src);
          // this.isImageFail = false;
          // this.isViewImage=false;
          // this.viewImage=null;
          // this.isImageSuccess = true;
          let element: HTMLElement = document.getElementById(
            'img'
          ) as HTMLElement;
          element.blur();
        };
      }
    } else {
      // this.isImageFail = true;
      // this.companyForm.value.imagePath = '';
      this.url1 = null;
      // this.isImageSuccess = false;
      // this.isViewImage=true;
    }
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
      if (event.target.files[0].size > 200000) {
        console.log('test img');

        Swal.fire({
          icon: 'error',
          text: 'Image size must be less than 200KB!',
        });
      }
      if (event.target.files[0].size <= 200000) {
        var reader = new FileReader();
        reader.readAsDataURL(event.target.files[0]);
        reader.onload = (event) => {
          this.imageSrc = event.target?.result;
          var img = new Image();
          this.url = event.target?.result as string;
          img.src = event.target?.result as string;
          this.announcementform.controls['imageSrc'].setValue(img.src);
          this.announcementform.controls['imagePath'].setValue(img.src);
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
      this.announcementform.value.imagePath = '';
      this.url = null;
      event.target.value = null;
      event.target.result = null;
      this.isImageSuccess = false;
      this.isViewImage = true;
    }
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
console.log(this.announcementform);

    if (this.announcementform.invalid) {
      return;
    }
    if (this.isAddMode) {
      this.addSection();
    } else {
      this.updateSection();
    }
  }

  addSection() {
    console.log(this.announcementform);

    var formDetails = this.announcementform.getRawValue();
    formDetails.attachment.push({
      "attachmentid": 0,
      "sectionid": 0,
      "path": formDetails.imagePath,
      "filename": this.filename,
      "filetype": this.filetype,
      "title": this.filename,
      "ispopup": false,
      "isredirected": false,
      "imagesrc":formDetails.imageSrc,
      "isactive": true,
      "imageflag": 1
    })
    this.sectionService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.sectionService
        .addSection(formDetails)
        .subscribe(
          (res: any) => {
            if (res.status === 200) {
              this.sectionService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>Announcement added successfully !!!</span>',
                'success'
              );
              this.routeToList();
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

  updateSection() {
    var formDetails = this.announcementform.getRawValue();
    formDetails.startdate = this.datepipe.transform(
      formDetails.startdate,
      'yyyy-MM-dd'
    );
    formDetails.enddate = this.datepipe.transform(
      formDetails.enddate,
      'yyyy-MM-dd'
    );
    formDetails.attachment.push({
      "attachmentid": this.attachmentId,
      "sectionid": this.id,
      "path":formDetails.imageSrc===null? this.path:formDetails.imagePath,
      "filename": this.filename,
      "filetype": this.filetype,
      "title": this.filename,
      "ispopup": false,
      "isredirected": false,
      "imagesrc":formDetails.imageSrc,
      "isactive": true,
      "imageflag": 1
    })
    this.sectionService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.sectionService
        .UpdateSection(formDetails)
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
                '<span>Announcement updated successfully !!!</span>',
                'success'
              );
            } else if (res.status === 400) {
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }

            this.sectionService.isLoadingSubject.next(false);
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
  routeToList(){
    this.router.navigate(['/admin/company-announcement']);
  }
}
