import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Ng2SmartRoutingModule } from './ng2-smart-routing.module';
import { Ng2SmartComponent } from './ng2-smart.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';

@NgModule({
  imports: [
    CommonModule,
    Ng2SmartRoutingModule,
    Ng2SmartTableModule
  ],
  declarations: [Ng2SmartComponent]
})
export class Ng2SmartModule { }
