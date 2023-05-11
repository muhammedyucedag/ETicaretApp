import { Component } from '@angular/core';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  constructor(private alertify: AlertifyService){}

  ngOnInit():void{
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
