import { ProfileSettingService } from './../../services/profile-setting.service';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { HeaderService } from 'src/app/containers/admin-layout/services/header.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-profile-setting',
  templateUrl: './profile-setting.component.html',
  styleUrl: './profile-setting.component.scss'
})
export class ProfileSettingComponent {

  private unsubscribe: Subscription = new Subscription();
  profileIsImageFail: boolean;
  profileIsImageSuccess: boolean;
  profileImageSrc: any;
  viewImage: any;
  profileImageUrl: any;
  profileIsViewImage: boolean = true;
  profileImagePath: any=null;


  ProfileImage:any="";
  /////////////////////////////////////////////////////////////////////

  wallpaperIsImageFail: boolean;
  wallpaperIsImageSuccess: boolean;
  wallpaperImageSrc: any;
  wallpaperge: any;
  wallpaperImageUrl: any;
  wallpaperIsViewImage: boolean = true;
  wallpaperImagePath: any=null;
  wallpaperImage:any="";
  constructor(
    private router: Router,
    private sanitizer: DomSanitizer,public profileSettingService:ProfileSettingService,
  public headerService:HeaderService){

      this.ProfileImage=localStorage.getItem('ProfileImage')!==null?localStorage.getItem('ProfileImage')?.toString():"./../../../../assets/images/user.png";

      this.wallpaperImage=localStorage.getItem('WallpaperPath')!==null?localStorage.getItem('WallpaperPath')?.toString():"../../../../assets/images/banner.png";

  }

    // Logo Upload Functionality
    profileUrl: any = null;

    onSelectFile(event: any) {
      if (event.target.files && event.target.files[0]) {
        console.log(event.target.files[0]);

        // if(event.target.files[0].size  < 50000 || event.target.files[0].size > 200000)
        if (event.target.files[0].size > 200000) {
          console.log('test img');

          Swal.fire({
            icon: 'error',
            text: 'Image size must be less than 200KB!',
          });
        }
        if (event.target.files[0].size <= 200000) {
          var reader = new FileReader();
          reader.readAsDataURL(event.target.files[0]);
          reader.onload = (event) => {
            this.profileImageSrc = event.target?.result;
            var img = new Image();
            this.profileUrl = event.target?.result as string;
            img.src = event.target?.result as string;
            this.profileImageSrc=img.src;
            this.profileImagePath=img.src;
            this.profileIsImageFail = false;
            this.profileIsViewImage = false;
            this.viewImage = null;
            this.profileIsImageSuccess = true;
            console.log(this.profileImageSrc);
            console.log(this.profileImagePath);

            let element: HTMLElement = document.getElementById(
              'img'
            ) as HTMLElement;
            element.blur();
          };
        }
      } else {
        this.profileIsImageFail = true;
        this.profileUrl = null;
        this.profileImageSrc=null;
        this.profileImagePath=null;
        event.target.value = null;
        event.target.result = null;
        this.profileIsImageSuccess = false;
        this.profileIsViewImage = true;
      }
    }

    uploadProfileImg(){


      let userId=localStorage.getItem('userId')!==null?localStorage.getItem('userId')?.toString():0;
  let obj=    {
        "userId": userId,
        "profileImage": this.profileImagePath,
        "profileSrc": this.profileImageSrc
      }
      this.unsubscribe.add(
        this.profileSettingService.UpdateProfileImage(obj).subscribe((res:any)=>{
console.log(res);
if(res.status===200){

  localStorage.setItem('ProfileImage', this.profileImageSrc);
  this.ProfileImage=this.profileImageSrc;
  this.headerService.isProfilechanged$.next(true);
  Swal.fire(
    'Success!',
    '<span>Profile Image Uploaded successfully !!!</span>',
    'success'
  );
}
if(res.status===400){
  Swal.fire(
    'Error!',
    '<span>Something went wrong, please try again later !!!</span>',
    'error'
  );
}
        })
      )

    }
    // Logo Upload Functionality
    wallpaperUrl: any = null;

    onSelectWallerpaperFile(event: any) {
      if (event.target.files && event.target.files[0]) {
        console.log(event.target.files[0]);

        // if(event.target.files[0].size  < 50000 || event.target.files[0].size > 200000)
        if (event.target.files[0].size > 200000) {
          console.log('test img');

          Swal.fire({
            icon: 'error',
            text: 'Image size must be less than 200KB!',
          });
        }
        if (event.target.files[0].size <= 200000) {
          var reader = new FileReader();
          reader.readAsDataURL(event.target.files[0]);
          reader.onload = (event) => {
            this.wallpaperImageSrc = event.target?.result;
            var img = new Image();
            this.wallpaperUrl = event.target?.result as string;
            img.src = event.target?.result as string;
            this.wallpaperImageSrc=img.src;
            this.wallpaperImagePath=img.src;
            this.wallpaperIsImageFail = false;
            this.wallpaperIsViewImage = false;
            this.viewImage = null;
            this.wallpaperIsImageSuccess = true;
            console.log(this.wallpaperImageSrc);
            console.log(this.wallpaperImagePath);

            let element: HTMLElement = document.getElementById(
              'img'
            ) as HTMLElement;
            element.blur();
          };
        }
      } else {
        this.wallpaperIsImageFail = true;
        this.wallpaperUrl = null;
        this.wallpaperImageSrc=null;
        this.wallpaperImagePath=null;
        event.target.value = null;
        event.target.result = null;
        this.wallpaperIsImageSuccess = false;
        this.wallpaperIsViewImage = true;
      }
    }

    uploadWallpaperImg(){


      let userId=localStorage.getItem('userId')!==null?localStorage.getItem('userId')?.toString():0;
  let obj=    {
        "userId": userId,
        "wallpaperPath": this.wallpaperImagePath,
        "isActive":true,
        "wallpaperSrc": this.wallpaperImageSrc
      }
      this.unsubscribe.add(
        this.profileSettingService.UpdateWallpaperImage(obj).subscribe((res:any)=>{
console.log(res);
if(res.status===200){

  localStorage.setItem('WallpaperPath', this.wallpaperImageSrc);
  this.wallpaperImage=this.wallpaperImageSrc;
  Swal.fire(
    'Success!',
    '<span>Wallpaper Image Uploaded successfully !!!</span>',
    'success'
  );
}
if(res.status===400){
  Swal.fire(
    'Error!',
    '<span>Something went wrong, please try again later !!!</span>',
    'error'
  );
}
        })
      )

    }
}
