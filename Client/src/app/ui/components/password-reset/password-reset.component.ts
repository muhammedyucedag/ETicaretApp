import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent extends BaseComponent {

  constructor(spinner: NgxSpinnerService, private userAuthService: UserAuthService, private alertifyService: AlertifyService) { 
    super(spinner)

  }

  ngOnInit(): void {
  }

  passwordReset(email: string){
    this.showSpinner(SpinnerType.BallAtom)
    this.userAuthService.passwordReset(email,() => {
      this.hideSpinner(SpinnerType.BallAtom)
      this.alertifyService.message("Mail başarılı bir şekilde gönderildi",{
        messageType : MessageType.Notify,
        position: Position.BottomCenter
      })
    })

  }

}
