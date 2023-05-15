import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { __values } from 'tslib';
import { List_Product } from 'src/app/contracts/list_product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private httpClientService: HttpClientService) { }

  create(product: Create_Product, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void) {
    this.httpClientService.post({
      controller:"product"
    },product)
    .subscribe(result=>{
      successCallBack();
    },(errorResponse:HttpErrorResponse)=>{
      const _error : Array<{key:string, value: Array<string>}> = errorResponse.error;
      let message = "";
      _error.forEach((v,index) =>{
        v.value.forEach((_v,_index)=>{
          message += `${_v}<br>`;
        });
      });
      errorCallBack(message);
    });
  }

  async read(page:number = 0, size:number = 5 ,successCallBack?: () => void, errorCallBack?:(errorMessage:string) => void){ //Gerite değer döndürmeyen parametre almayan bir fonksiyon
    const promiseData : Promise<{ totalCount:number; products:List_Product[]}> = this.httpClientService.get<{totalCount:number; products: List_Product[]}>({
      controller:"product",
      queryString: `page=${page}&size=${size}`
    }).toPromise();

    promiseData.then(d => successCallBack())
      .catch((errorResponse:HttpErrorResponse) => errorCallBack(errorResponse.message)) 
    //Catch hata then başarılı sonuc varsa

    return await promiseData;
   }
}
