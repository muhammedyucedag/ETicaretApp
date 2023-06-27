import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsModule } from './products/products.module';
import { BasketsModule } from './baskets/baskets.module';
import { HomeModule } from './home/home.module';
import { RegisterModule } from './register/register.module';


  
@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ProductsModule,
    HomeModule,
    BasketsModule,
    RegisterModule,
    //LoginModule
  ],
  exports: [
    BasketsModule
  ]
})
export class ComponentsModule { }
