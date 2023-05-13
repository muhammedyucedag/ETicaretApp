import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Product } from 'src/app/contracts/product';
import { HttpClientService } from 'src/app/services/common/http-client.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent{
  constructor(spinner : NgxSpinnerService, private httpClientService: HttpClientService){
    super(spinner)
  }

  ngOnInit():void{
    this.showSpinner(SpinnerType.BallAtom)

    this.httpClientService.get<Product[]>({
      controller: "product"
    }).subscribe(data => console.log(data));

    // this.httpClientService.post({
    //   controller: "product"
    // },{
    //   name:"Kalem",
    //   stock: 100,
    //   price : 15
    // }).subscribe();

    // this.httpClientService.put({
    //   controller: "product",
    // },{
    //   id:"29109e3c-39d3-4b74-892f-5164d615524e",
    //   name:"Renkl Kalem",
    //   stock:"550",
    //   price:"13.5"
    // }).subscribe()

    // this.httpClientService.delete({
    //   controller:"product"
    // },"29109e3c-39d3-4b74-892f-5164d615524e").subscribe();

    // this.httpClientService.get({
    //   fullEndPoint:"https://jsonplaceholder.typicode.com/posts"
    // }).subscribe(data=>console.log(data));
  }

}
