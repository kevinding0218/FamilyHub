import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MemberListComponent } from './member-list.component';

const routes: Routes = [{
  path: '',
  component: MemberListComponent,
  data: {
    title: 'Member List Simple',
    icon: 'icon-users'
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MemberListRoutingModule { }
