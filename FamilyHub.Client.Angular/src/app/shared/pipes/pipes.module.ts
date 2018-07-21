import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MomentPipe } from './moment.pipe';
import { ToStringPipe } from './toString.pipe';
import { DataFilterPipe } from './data-filter.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [DataFilterPipe, ToStringPipe, MomentPipe],
  exports: [DataFilterPipe, ToStringPipe, MomentPipe]
})
export class PipesModule { }
