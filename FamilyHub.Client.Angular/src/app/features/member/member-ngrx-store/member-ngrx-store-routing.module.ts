import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MemberNgrxStoreComponent } from './member-ngrx-store.component';

const routes: Routes = [{
  path: '',
  component: MemberNgrxStoreComponent,
  data: {
    title: 'Member List Advance',
    icon: 'icon-users'
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MemberNgrxStoreRoutingModule { }
