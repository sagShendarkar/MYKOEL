import { VacancyService } from './../../../services/vacancy.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import { PaginationParams } from 'src/app/models/paginationParams';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-vacancy-list',
  templateUrl: './vacancy-list.component.html',
  styleUrl: './vacancy-list.component.scss'
})
export class VacancyListComponent {
  pagination: any;
  pageIndex = 0;
  private unsubscribe: Subscription = new Subscription();
  paginationParams: PaginationParams;
  isNoData: boolean = false;

  searchUpdate = new Subject<string>();
  constructor(
    private router: Router,
    public vacancyService: VacancyService,
  ) {}


  ngOnInit(): void {
    this.paginationParams =
      this.vacancyService.paginationParams;
    this.paginationParams.pageNumber = 1;
    this.loadVacancyList();

    this.searchUpdate.pipe(
      debounceTime(500),
      distinctUntilChanged())
      .subscribe(value => {
        console.log(value);
        this.paginationParams.searchPagination=value;
        this.loadVacancyList();
      });
  }

  loadVacancyList() {
    this.unsubscribe.add(
      this.vacancyService
        .getVacancyPostingList(this.paginationParams)
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
            this.vacancyService.VacancyPostingList$.next(
              res.result
            );
            this.pagination = res.pagination;
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
  JobDescriptionById(id=0) {
    this.unsubscribe.add(
      this.vacancyService
        .getJobDescriptionById(id)
        .subscribe(
          (res: any) => {
            console.log(res);
            // filepath

    window.open(res.filepath, '_blank');
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
  pageChanged(event: any) {
    this.vacancyService.paginationParams.pageNumber =
      event.page;
    this.loadVacancyList();
  }

  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
  }
}
