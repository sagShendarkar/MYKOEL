import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminLayoutComponent, DefaultLayoutComponent } from './containers';
import { Page404Component } from './views/pages/page404/page404.component';
import { Page500Component } from './views/pages/page500/page500.component';
import { LoginComponent } from './views/pages/login/login.component';
import { RegisterComponent } from './views/pages/register/register.component';
import { MoodCheckComponent } from './views/pages/mood-check/mood-check.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  // {
  //   path: 'admin',
  //   redirectTo: 'dashboard',
  //   pathMatch: 'full'
  // },
  // {
  //   path: 'admin',
  //   component: DefaultLayoutComponent,
  //   data: {
  //     title: 'Home'
  //   },
  //   children: [
  //     {
  //       path: 'dashboard',
  //       loadChildren: () =>
  //         import('./views/dashboard/dashboard.module').then((m) => m.DashboardModule)
  //     },
  //     {
  //       path: 'theme',
  //       loadChildren: () =>
  //         import('./views/theme/theme.module').then((m) => m.ThemeModule)
  //     },
  //     {
  //       path: 'base',
  //       loadChildren: () =>
  //         import('./views/base/base.module').then((m) => m.BaseModule)
  //     },
  //     {
  //       path: 'buttons',
  //       loadChildren: () =>
  //         import('./views/buttons/buttons.module').then((m) => m.ButtonsModule)
  //     },
  //     {
  //       path: 'forms',
  //       loadChildren: () =>
  //         import('./views/forms/forms.module').then((m) => m.CoreUIFormsModule)
  //     },
  //     {
  //       path: 'charts',
  //       loadChildren: () =>
  //         import('./views/charts/charts.module').then((m) => m.ChartsModule)
  //     },
  //     {
  //       path: 'icons',
  //       loadChildren: () =>
  //         import('./views/icons/icons.module').then((m) => m.IconsModule)
  //     },
  //     {
  //       path: 'notifications',
  //       loadChildren: () =>
  //         import('./views/notifications/notifications.module').then((m) => m.NotificationsModule)
  //     },
  //     {
  //       path: 'widgets',
  //       loadChildren: () =>
  //         import('./views/widgets/widgets.module').then((m) => m.WidgetsModule)
  //     },
  //     {
  //       path: 'pages',
  //       loadChildren: () =>
  //         import('./views/pages/pages.module').then((m) => m.PagesModule)
  //     },
  //   ]
  // },
  {
    path: '',
    component: AdminLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'home',
        loadChildren: () =>
          import('./views/landing/landing.module').then((m) => m.LandingModule)
      },
      {
        path: 'my-page',
        loadChildren: () =>
          import('./views/mypage/mypage.module').then((m) => m.MypageModule)
      },
      {
        path: 'departments',
        loadChildren: () =>
          import('./views/departments/departments.module').then((m) => m.DepartmentsModule)
      },
      {
        path: 'admin',
        loadChildren: () =>
          import('./views/admin/admin.module').then((m) => m.AdminModule)
      },
      {
        path: 'buttons',
        loadChildren: () =>
          import('./views/buttons/buttons.module').then((m) => m.ButtonsModule)
      },
      {
        path: 'forms',
        loadChildren: () =>
          import('./views/forms/forms.module').then((m) => m.CoreUIFormsModule)
      },
      {
        path: 'charts',
        loadChildren: () =>
          import('./views/charts/charts.module').then((m) => m.ChartsModule)
      },
      {
        path: 'icons',
        loadChildren: () =>
          import('./views/icons/icons.module').then((m) => m.IconsModule)
      },
      {
        path: 'notifications',
        loadChildren: () =>
          import('./views/notifications/notifications.module').then((m) => m.NotificationsModule)
      },
      {
        path: 'widgets',
        loadChildren: () =>
          import('./views/widgets/widgets.module').then((m) => m.WidgetsModule)
      },
      {
        path: 'pages',
        loadChildren: () =>
          import('./views/pages/pages.module').then((m) => m.PagesModule)
      },
    ]
  },
  {
    path: '404',
    component: Page404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: Page500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'mood-check',
    component: MoodCheckComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {path: '**', redirectTo: 'dashboard'}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking'
      // relativeLinkResolution: 'legacy'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
