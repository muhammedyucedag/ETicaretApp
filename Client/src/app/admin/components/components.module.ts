import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerModule } from './customer/customer.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { OrderModule } from './order/order.module';
import { ProductsModule } from './products/products.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ProductsModule,
    CustomerModule,
    OrderModule,
    DashboardModule
  ]
})
export class ComponentsModule { }
