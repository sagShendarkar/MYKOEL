import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { VacancyService } from '../../../services/vacancy.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import Swal from 'sweetalert2';
import { EnumDDService } from 'src/app/services/common/enumDD.service';

@Component({
  selector: 'app-add-vacancy',
  templateUrl: './add-vacancy.component.html',
  styleUrl: './add-vacancy.component.scss'
})
export class AddVacancyComponent {

  isAddMode: Boolean = true;
  submitted = false;
  deletedAttachmentIds: number[] = [];
  private unsubscribe: Subscription = new Subscription();
  vacancyform: FormGroup;
  id!: number;
  constructor(
    public vacancyService: VacancyService,
    public enumDDService: EnumDDService,
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
      this.getDepartmentDD();
      this.getGradeDD();
      this.getJobStatusDD();
      this.getJobTypeDD();
      if (!this.isAddMode) {
        this.getDetailsById(+this.id);
      }
    }

  getDetailsById(id = 0) {
    this.unsubscribe.add(
      this.vacancyService
        .getVacancyDetail(id)
        .subscribe((x) => {
          console.log(x);
          x.posteddate = this.datepipe.transform(x.posteddate,'dd MMM yyyy');
          x.closingdate = this.datepipe.transform(x.closingdate,'dd MMM yyyy');
          this.vacancyform.patchValue(x);
console.log(this.vacancyform.valid);



        })
    );
  }

    getDepartmentDD(){
      this.unsubscribe.add(
        this.vacancyService.getDepartmentDD().subscribe((res) => {
          this.vacancyService.departmentDD$.next(res);
        })
      );
    }
    getGradeDD(){
      this.unsubscribe.add(
        this.vacancyService.getGradeDD().subscribe((res) => {
          this.vacancyService.gradeDD$.next(res);
        })
      );
    }

    getJobStatusDD() {
    this.unsubscribe.add(
      this.enumDDService.getJobStatusDD().subscribe((res) => {
        this.enumDDService.jobStatusDD$.next(res);
      })
    );
  }
  getJobTypeDD() {
    this.unsubscribe.add(
      this.enumDDService.getJobTypeDD().subscribe((res) => {
        this.enumDDService.jobTypeDD$.next(res);
      })
    );
  }
  initializeForm() {
    this.vacancyform = new FormGroup({

      vacancyid: new FormControl(0),
      grade: new FormControl(null, Validators.required),
      jobtitle: new FormControl('', Validators.required),
      vacancycount: new FormControl('', Validators.required),
      department: new FormControl(null, Validators.required),
      location: new FormControl('', Validators.required),
      jobtype: new FormControl(null, Validators.required),
      salaryrange: new FormControl('', Validators.required),
      jobdesc: new FormControl(null,),

      requirments: new FormControl(null, Validators.required),
      posteddate: new FormControl(null, Validators.required),
      closingdate: new FormControl(null, Validators.required),
      contactinfo: new FormControl(null, Validators.required),
      status: new FormControl(1, Validators.required),
      isactive: new FormControl(true, ),
      pdfstring: new FormControl(null, ),
      pdfpath: new FormControl(null, Validators.required),
    });
  }



  onChange(event: any) {

    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (event) => {
      // this.imageSrc = event.target?.result;
      var img = new Image();
      // this.url = event.target?.result as string;
      img.src = event.target?.result as string;
    this.vacancyform.controls['pdfstring'].setValue(event.target?.result as string);
    this.vacancyform.controls['jobdesc'].setValue(event.target?.result as string);
    this.vacancyform.controls['pdfpath'].setValue(event.target?.result as string);


      let element: HTMLElement = document.getElementById(
        'pdf'
      ) as HTMLElement;
      element.blur();
    };
  }
  openFile(url: string) {
    window.open(url, '_blank');
  }
  onSubmit() {
    this.submitted = true;

    if (this.vacancyform.invalid) {
      return;
    }
    if (this.isAddMode) {
      this.addvacancy();
    } else {
      this.updatevacancy();
    }
  }

  addvacancy() {
    console.log(this.vacancyform);

    var formDetails = this.vacancyform.getRawValue();

    this.vacancyService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.vacancyService
        .addVacancy(formDetails)
        .subscribe(
          (res: any) => {
            if (res.status === 200) {
              this.vacancyService.isLoadingSubject.next(false);
              Swal.fire(
                'Success!',
                '<span>Vacancy added successfully !!!</span>',
                'success'
              );
              this.routeToList();
            } else if (res.status === 400) {
              this.vacancyService.isLoadingSubject.next(false);
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }
          },
          (err) => {
            this.vacancyService.isLoadingSubject.next(false);
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
    var formDetails = this.vacancyform.getRawValue();
    formDetails.posteddate = this.datepipe.transform(
      formDetails.posteddate,
      'yyyy-MM-dd'
    );
    formDetails.closingdate = this.datepipe.transform(
      formDetails.closingdate,
      'yyyy-MM-dd'
    );
    this.vacancyService.isLoadingSubject.next(true);
    this.unsubscribe.add(
      this.vacancyService
        .UpdateVacancy(formDetails)
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
                '<span>Vacancy updated successfully !!!</span>',
                'success'
              );
            } else if (res.status === 400) {
              Swal.fire(
                'Error!',
                '<span>Something went wrong, please try again later !!!</span>',
                'error'
              );
            }

            this.vacancyService.isLoadingSubject.next(false);
          },
          (err) => {
            this.vacancyService.isLoadingSubject.next(false);
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
    this.router.navigate(['/admin/vacancy-list']);
  }
  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
  }
}
