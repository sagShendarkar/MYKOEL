import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, finalize, map } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnumDDService {

  baseUrl = environment.apiUrl1;


  isLoading$: Observable<boolean>;
  isLoadingSubject: BehaviorSubject<boolean>;

  categoryDD$=  new BehaviorSubject<any>([]);
  constructor(private http:HttpClient,private router: Router) {

    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
  }


  getCategoryDD()
  {
    return this.http.get<any>(this.baseUrl+'Enum/CategoryDD');
  }
}
