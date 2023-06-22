import { Component, ViewChild } from '@angular/core';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
import { AuthService } from './services/common/auth.service';
import { Router } from '@angular/router';
import { HttpClientService } from './services/common/http-client.service';
import { ComponentType, DynmaicLoadComponentService } from './services/common/dynmaic-load-component.service';
import { DynamicLoadComponentDirective } from './directives/common/dynamic-load-component.directive';
declare var $: any  //Jquery Tanımlaması

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  @ViewChild(DynamicLoadComponentDirective, {static: true})
  dynamicLoadComponentDirective:DynamicLoadComponentDirective;


  constructor(public authService:AuthService, private toastrService: CustomToastrService, private router:Router, private dynamicLoadComponentService:DynmaicLoadComponentService){

    // httpClientService.get({
    //   controller: "baskets"
    // }).subscribe(data => {
    //   debugger;
    // });

    // authService.identityCheck();
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

  loadComponent(){
    this.dynamicLoadComponentService.loadComponent(ComponentType.BasketsComponent,this.dynamicLoadComponentDirective.viewContainerRef);
  }
}

