

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthLayoutComponent } from './shared/layout/auth-layout/auth-layout.component';
import { AdminLayoutComponent } from './shared/layout/admin-layout/admin-layout.component';

const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: 'finance',
        pathMatch: 'full'
      },
      {
        path: 'member',
        loadChildren: './features/member/member.module#MemberModule'
      },
      {
        path: 'finance',
        loadChildren: './features/finance/finance.module#FinanceModule'
      },
      {
        path: 'simple-page',
        loadChildren: './features/simple-page/simple-page.module#SimplePageModule'
      }
    ]
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: 'coming-soon',
        loadChildren: './features/coming-soon/coming-soon.module#ComingSoonModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
