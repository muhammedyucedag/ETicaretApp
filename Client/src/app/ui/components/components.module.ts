import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsModule } from './products/products.module';
import { BasketsModule } from './baskets/baskets.module';
import { HomeModule } from './home/home.module';


  
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ProductsModule,
    BasketsModule,
    HomeModule   
  ]
})
export class ComponentsModule { }
