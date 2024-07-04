import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-company-announcement',
  templateUrl: './company-announcement.component.html',
  styleUrl: './company-announcement.component.scss'
})
export class CompanyAnnouncementComponent {
constructor(private route: ActivatedRoute){

}
}
