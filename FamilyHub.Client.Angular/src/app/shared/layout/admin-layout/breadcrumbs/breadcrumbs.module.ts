import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { BreadcrumbsComponent } from './breadcrumbs.component';

@NgModule({
  declarations: [BreadcrumbsComponent],
  imports: [
    CommonModule,
    RouterModule
  ],  
  exports: [RouterModule, BreadcrumbsComponent]
})
export class BreadcrumbsModule { }
