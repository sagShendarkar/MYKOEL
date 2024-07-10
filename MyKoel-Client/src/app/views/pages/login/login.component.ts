
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { AuthService } from './../../../services/auth/auth.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  isInvalidUser: boolean = false;
  loginForm!: FormGroup;
  submitted = false;
  constructor(public authService: AuthService, private router: Router) {
    if (this.authService.isTokenAvailable()) {
      this.router.navigate(['/']);
    }
  }
  ngOnInit(): void {
    this.initializeForm();
  }
  initializeForm() {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  login() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }


    this.authService.isLoadingSubject.next(true);
    this.authService.login(this.loginForm.value).subscribe((res: any) => {
      if (res.status == 200) {
        this.isInvalidUser = false;
        localStorage.setItem('token', res.Token);
        localStorage.setItem('userId', res.UserId);
        localStorage.setItem('username', res.Username);


        let menuList: any[] = [];
        // let accessMenu = res[0].userAccessList;

//         accessMenu.forEach((element: any) => {
//           // console.log(element);
//           if (menuList.length > 0) {
//             let index = menuList.findIndex(
//               (a) => a.MenuGroupId === element.MenuGroupId
//             );
//             if (index === -1) {
//               menuList.push(element);
//             } else {
//               menuList[index].MenuList.push(element.MenuList[0]);
//             }
//           } else {
//             menuList.push(element);
//           }
//         });

//         menuList.forEach((element: any) => {
//           let navobj: any = {
//             name: element.MenuGroupName,
//             url: element.MenuGroupAction,
//             iconComponent: { name:  element.MenuGroupIcon },
//           };
//           if (element.IsChild) {
//             navobj['children']=[]
//             element.MenuList.forEach((ele: any) => {
//               navobj['children'].push({
//                 'name':ele.MenuName,
//                 'url':ele.MenuAction
//               });
//             });
//           }
//           this.navItems.push(navobj);
//         });

// localStorage.setItem('navItems',JSON.stringify(this.navItems));

this.authService.isLoadingSubject.next(false);
        this.router.navigateByUrl("/mood-check");
      } else if (res.status == 400) {

this.authService.isLoadingSubject.next(false);
        this.isInvalidUser = true;
        setTimeout(() => {
          this.isInvalidUser = false;
        }, 2000);
      }
    },
  (err)=>{

this.authService.isLoadingSubject.next(false);
Swal.fire(
  'Error!',
  '<span>Something went wrong, please try again later !!!</span>',
  'error'
);
  });
  }
  routeToForgetPassword(){

    this.router.navigateByUrl("/forgot-password");
  }
}
