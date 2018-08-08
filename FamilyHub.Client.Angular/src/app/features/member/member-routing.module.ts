import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [{
  path: '',
  data: {
    title: 'Member',
    status: true
  },
  children: [
    {
      path: '',
      redirectTo: 'member-list',
      pathMatch: 'full'
    },
    {
      path: 'member-list',
      loadChildren: './member-list/member-list.module#MemberListModule'
    },
    {
      path: 'member-ngrx-store',
      loadChildren: './member-ngrx-store/member-ngrx-store.module#MemberNgrxStoreModule'
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MemberRoutingModule { }
