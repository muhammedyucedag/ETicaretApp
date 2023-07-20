import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Menu } from 'src/app/contracts/application-configurations/menu';
import { Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private httpClientService:HttpClientService) { }

  async getAuthorizeDefinitionEndPoints(){
    const observable : Observable<Menu[]> = this.httpClientService.get({
      controller: "ApplicationServices"
    });

    return await firstValueFrom(observable);
  }
}
