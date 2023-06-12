import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { HubUrls } from 'src/app/constants/hub-urls';
import { ReceiveFunctions } from 'src/app/constants/receive-functions';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { SignalRService } from 'src/app/services/common/signalr.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent  extends BaseComponent{

  constructor(private alertify: AlertifyService, spinner: NgxSpinnerService, private signalRService: SignalRService){
    super(spinner)
    signalRService.start(HubUrls.ProductHub)
  }

  ngOnInit():void{
    this.signalRService.on(ReceiveFunctions.ProductAddedMessageReceiveFunction, message => {
      this.alertify.message(message,{
        messageType:MessageType.Notify,
        position:Position.BottomCenter
      })
    });
  }

  m(){
    this.alertify.message("Yönetim paneline hoşgeldiniz",{
      messageType: MessageType.Success,
      delay : 5,
      position : Position.TopCenter,
      dismissOthers : true
    })
  }

  d(){
    this.alertify.dismiss();
  }
}
