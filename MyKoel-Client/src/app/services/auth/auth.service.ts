import { AuthModel } from './../../models/auth.model';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authLocalStorageToken = `${environment.appVersion}-${environment.USERDATA_KEY}`;

  baseUrl = environment.apiUrl1;
  isLoading$: Observable<boolean>;
  isLoadingSubject: BehaviorSubject<boolean>;
  constructor(private http:HttpClient,private router: Router) {

    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.isLoading$ = this.isLoadingSubject.asObservable();
   }


  login(model:any)
  {
    return this.http.post(this.baseUrl + 'Account/login', model);

  }

  moodToday(model:any)
  {
    return this.http.post(this.baseUrl + 'MoodToday/AddMoodToday', model);

  }

  validateEmail(email:any)
  {
    return this.http.get(this.baseUrl + 'Account/ForgotPassword?Email='+email);

  }
  changePassword(model:any)
  {
    let params=new HttpParams();
    params=params.append('UserId',model.UserId.toString());
    params=params.append('Token',model.Token.toString());
    params=params.append('NewPassword',model.NewPassword.toString());
    return this.http.get(this.baseUrl + 'Account/ResetPassword', {params});

  }
  isTokenAvailable(){
    let token= localStorage.getItem('token');
    if(token!==undefined&&token!==null){
return true
    }else{
   return   false
    }
  }
  isMoodFilled(){
    let IsMoodFilled= localStorage.getItem('IsMoodFilled');
    if(IsMoodFilled==='YES'){
return true
    }else   if(IsMoodFilled==='NO'){
   return   false
    }else{ 
   return   false
    }
  }
  logout(){

    localStorage.clear();

  this.router.navigateByUrl("/login")
  }

  public  getAuthFromLocalStorage(): AuthModel | undefined {
    try {
      const lsValue = localStorage.getItem(this.authLocalStorageToken);
      if (!lsValue) {
        return undefined;
      }

      const authData = JSON.parse(lsValue);
      return authData;
    } catch (error) {
      console.error(error);
      return undefined;
    }
  }

}
