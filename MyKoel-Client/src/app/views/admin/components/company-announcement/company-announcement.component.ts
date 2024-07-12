import { SectionService } from './../../services/section.service';
import { PaginationParams } from './../../../../models/paginationParams';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-company-announcement',
  templateUrl: './company-announcement.component.html',
  styleUrl: './company-announcement.component.scss'
})
export class CompanyAnnouncementComponent {
  pagination: any;
  pageIndex = 0;
  private unsubscribe: Subscription = new Subscription();
  paginationParams: PaginationParams;
  isNoData: boolean = false;

  searchUpdate = new Subject<string>();
  constructor(
    private router: Router,
    public sectionService: SectionService,
  ) {}

  ngOnInit(): void {
    this.paginationParams =
      this.sectionService.paginationParams;
    this.paginationParams.pageNumber = 1;
    this.paginationParams.flag = "Announcement";
    this.loadSectionList();

    this.searchUpdate.pipe(
      debounceTime(500),
      distinctUntilChanged())
      .subscribe(value => {
        console.log(value);
        this.paginationParams.searchPagination=value;
        this.loadSectionList();
      });
  }

  loadSectionList() {
    this.unsubscribe.add(
      this.sectionService
        .getSectionTransactionList(this.paginationParams)
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
            this.sectionService.sectionTransactionList$.next(
              res.result
            );
            this.pagination = res.pagination;
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
  pageChanged(event: any) {
    this.sectionService.paginationParams.pageNumber =
      event.page;
    this.loadSectionList();
  }

  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
  }
}
