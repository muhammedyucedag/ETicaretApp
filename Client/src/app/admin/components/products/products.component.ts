import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
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
    this.httpClientService.get({
      controller: "product"
    }).subscribe(data => console.log(data))
  }

}
