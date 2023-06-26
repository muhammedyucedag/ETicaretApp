import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from './layout/layout.module';
import { ComponentsModule } from './components/components.module';
import { ListComponent } from './component/order/list/list.component';



@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    LayoutModule
  ],
  exports:[
    LayoutModule
  ]
})
export class AdminModule { }
