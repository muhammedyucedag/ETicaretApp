import { Component } from '@angular/core';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
import { AuthService } from './services/common/auth.service';
import { Router } from '@angular/router';
import { HttpClientService } from './services/common/http-client.service';
declare var $: any  //Jquery Tanımlaması

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService:AuthService, private toastrService: CustomToastrService, private router:Router, private httpClientService : HttpClientService){
    
    httpClientService.get({
      controller: "baskets"
    }).subscribe(data => {
      debugger;
    });

    authService.identityCheck();
  }
  signOut(){
    localStorage.removeItem("accessToken");
    this.authService.identityCheck();
    this.router.navigate([""]);
    this.toastrService.message("Oturum kapatılmıştır!", "Oturum Kapatıldı",{
      messageType: ToastrMessageType.Warning,
      position: ToastrPosition.BottomFullWidth
    })
  }
}

