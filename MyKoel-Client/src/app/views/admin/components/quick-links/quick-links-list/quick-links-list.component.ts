import { QuickLinksService } from './../../../services/quick-links.service';

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject, Subscription } from 'rxjs';
import { PaginationParams } from 'src/app/models/paginationParams';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-quick-links-list',
  templateUrl: './quick-links-list.component.html',
  styleUrl: './quick-links-list.component.scss'
})
export class QuickLinksListComponent {
  pagination: any;
  pageIndex = 0;
  private unsubscribe: Subscription = new Subscription();
  paginationParams: PaginationParams;
  isNoData: boolean = false;

  searchUpdate = new Subject<string>();
  constructor(
    private router: Router,
    public quickLinksService: QuickLinksService,
  ) {}


  ngOnInit(): void {
    this.paginationParams =
      this.quickLinksService.paginationParams;
    this.paginationParams.pageNumber = 1;
    this.paginationParams.flag = "Quick Links";
    this.quickLinksList();

    this.searchUpdate.pipe(
      debounceTime(500),
      distinctUntilChanged())
      .subscribe(value => {
        console.log(value);
        this.paginationParams.searchPagination=value;
        this.quickLinksList();
      });
  }

  quickLinksList() {
    this.unsubscribe.add(
      this.quickLinksService
        .getMenuHierarchyList(this.paginationParams)
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
            this.quickLinksService.quickLinksMenuList$.next(
              res.result
            );
            this.pagination = res.pagination;
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
  pageChanged(event: any) {
    this.quickLinksService.paginationParams.pageNumber =
      event.page;
    this.quickLinksList();
  }

  ngOnDestroy(): void {
    this.unsubscribe.unsubscribe();
    this.quickLinksService.quickLinksMenuList$.next([]);
  }
}
