import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  isDisplayBreadcrumb$=  new BehaviorSubject<boolean>(true);
  constructor() { }
}
