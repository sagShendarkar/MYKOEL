import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { SectionService } from '../../services/section.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-view-detailed-news',
  templateUrl: './view-detailed-news.component.html',
  styleUrl: './view-detailed-news.component.scss'
})
export class ViewDetailedNewsComponent {

  private unsubscribe: Subscription = new Subscription();
  id!: number;
    newsDetails:any;
  constructor(public sectionService: SectionService,

    private sanitizer: DomSanitizer,
    private route: ActivatedRoute,
    public datepipe: DatePipe,
    private router: Router,){}
    ngOnInit(): void {
      //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
      //Add 'implements OnInit' to the class.

      this.id = this.route.snapshot.params['id'];

        // this.formName = 'Edit';
        this.getDetailsById(+this.id);

    }

    getDetailsById(id = 0) {
      this.unsubscribe.add(
        this.sectionService
          .getSectionDetail(id)
          .subscribe((x) => {
            console.log(x);
            x.enddate = this.datepipe.transform(x.enddate,'dd MMM yyyy');
            x.startdate = this.datepipe.transform(x.startdate,'dd MMM yyyy');

            this.newsDetails=x;

          })
      );
    }
    ngOnDestroy(): void {
      this.unsubscribe.unsubscribe();
    }
}
