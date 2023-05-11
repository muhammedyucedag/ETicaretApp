import { Injectable } from '@angular/core';
declare var alertify: any; //uygulamanın her yerinde bu kütüphaneyi kullanıyoruz


@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() { }

  //message(message: string, messageType: MessageType, position: Position, delay: Number = 3, dismissOthers:boolean=false)
  message(message: string, Options: Partial<AlertifyOptions>)
  {
    alertify.set('notifier','delay', Options.delay);
    alertify.set('notifier','position', Options.position);
    const msj = alertify[Options.messageType](message)
    if(Options.dismissOthers)
    msj.dismissOthers();
  }

  // tek tıklama ile tüm notifcation mesajları siler
  dismiss(){
    alertify.dismissAll();
  }
}

// tsconfig.json da bulunan "strict" özelliğini false yapıyoruz ve sıkı yönetimi yumuşatıyoruz
export class AlertifyOptions{
  messageType : MessageType = MessageType.Message;
  position : Position = Position.TopCenter;
  delay: number = 3;
  dismissOthers:boolean = false;
}

// enum ile mesaj tür belirliyoruz
export enum MessageType{
  Error = "error",
  Message = "message",
  Notify = "notify",
  Success = "success",
  Warning = "warning"
}

export enum Position {
  TopCenter = "top-center",
  TopRight = "top-right",
  TopLeft = "top-left",
  BottomRight = "bottom-right",
  BottomCenter = "bottom-center",
  bottomLeft = "bottom-left"
}
