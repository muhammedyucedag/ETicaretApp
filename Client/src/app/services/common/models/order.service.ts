import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Order } from 'src/app/contracts/order/create_order';
import { Observable, firstValueFrom } from 'rxjs';
import { List_Order } from 'src/app/contracts/order/list_order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClientService : HttpClientService) { }

  async create(order : Create_Order){
    const observable : Observable<any> = this.httpClientService.post({
      controller : "orders"
    },order);

    await firstValueFrom(observable);
  }

  async getAllOrders(): Promise<List_Order[]>{
    const observable : Observable<List_Order[]> = this.httpClientService.get({
      controller : "orders"
    });

    return await firstValueFrom(observable);
  }
}
