import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProfileSettingService {

  baseUrl = environment.apiUrl1;
  isLoading$: Observable<boolean>;
  isLoadingSubject: BehaviorSubject<boolean>;
  constructor(private http:HttpClient,private router: Router) {

    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
   }

   UpdateProfileImage(model:any)
  {
    return this.http.post(this.baseUrl + 'User/UpdateProfileImage', model);

  }
   UpdateWallpaperImage(model:any)
  {
    return this.http.post(this.baseUrl + 'Wallpaper/AddWallPaper', model);

  }

}
