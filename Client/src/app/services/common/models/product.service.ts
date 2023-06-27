import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { __values } from 'tslib';
import { List_Product } from 'src/app/contracts/list_product';
import { Observable, async, first, firstValueFrom } from 'rxjs';
import { List_Product_Image } from 'src/app/contracts/list_product_images';

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
  
  async read(page:number = 0, size:number = 5 ,successCallBack?: () => void, errorCallBack?:(errorMessage:
    string) => void): Promise<{totalProductCount: number; products: List_Product[]}>{ //Gerite değer döndürmeyen parametre almayan bir fonksiyon
    const promiseData : Promise<{ totalProductCount:number; products:List_Product[]}> = 
    this.httpClientService.get<{totalProductCount:number; products: List_Product[]}>({
      controller:"product",
      queryString: `page=${page}&size=${size}`
    }).toPromise();

    promiseData.then(d => successCallBack())
      .catch((errorResponse:HttpErrorResponse) => errorCallBack(errorResponse.message)) 
    //Catch hata then başarılı sonuc varsa

    return await promiseData; 
  } 

  async delete(id: string){
    const deleteObservable: Observable<any> = this.httpClientService.delete<any>({
      controller:"product"
    },id)

    await firstValueFrom(deleteObservable)
  }

  async readImages(id: string, successCallBack?: () => void): Promise<List_Product_Image[]> {
    const getObservable: Observable<List_Product_Image[]> = this.httpClientService.get<List_Product_Image[]>({
      action:"getproductimages",
      controller: "product"
    },id);

    const image:List_Product_Image[] = await firstValueFrom(getObservable)
    successCallBack();
    return image
  }

  async deleteImage(id: string, imageId: string, successCallBack?: () => void){
    const deleteObservable = this.httpClientService.delete({
      action:"deleteproductimage",
      controller:"product",
      queryString:`imageId=${imageId}`
    },id)
    await firstValueFrom(deleteObservable);
  }
  
  async changeShowcaseImage(imageId: string, productId: string, successCallBack?: () => void) : Promise<void>{
    const changeShowcaseImageObservable =  this.httpClientService.get({
      controller: "product",
      action: "ChangeShowcaseImage",
      queryString: `imageId=${imageId}&productId=${productId}`,
    });
    await firstValueFrom(changeShowcaseImageObservable);
    successCallBack();
  }
}
