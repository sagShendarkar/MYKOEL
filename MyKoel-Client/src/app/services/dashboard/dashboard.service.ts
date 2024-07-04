import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient,private router: Router) { }

 
  dummy(){
    
    return this.http.get('https://dummyjson.com/products/1');
     
  }
 
}
