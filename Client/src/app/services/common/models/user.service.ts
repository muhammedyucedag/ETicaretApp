import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { User } from 'src/app/entities/user';
import { Create_User } from 'src/app/contracts/users/create_user';
import { Observable, firstValueFrom } from 'rxjs';
import { Token } from '@angular/compiler';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { SocialUser } from '@abacritt/angularx-social-login';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  constructor(private httpClientService: HttpClientService, private toastrService : CustomToastrService ) { }

  async create(user:User): Promise<Create_User>{
    const observable : Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller:"Users"
    },user);

    return await firstValueFrom(observable) as Create_User;
  }


}
