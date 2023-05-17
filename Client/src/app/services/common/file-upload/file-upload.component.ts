import { Component, Input } from '@angular/core';
import { NgxFileDropEntry,FileSystemFileEntry,FileSystemDirectoryEntry } from 'ngx-file-drop';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent {

  constructor(
    private httpCilentService: HttpClientService,
    private alertifyService: AlertifyService,
    private customToastrService: CustomToastrService) {
  }

  public files: NgxFileDropEntry[];

  @Input() options:Partial<FileUploadOptions>;
  

  // seçilen tüm dosyalar fileData içerisine koyuldu
  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData : FormData = new FormData();
    for(const file of  files){
      (file.fileEntry as FileSystemFileEntry).file((_file:File)=>{
        fileData.append(_file.name,_file,file.relativePath);
      })
    }
    // gönderme opersayonu (sunucuya)
    this.httpCilentService.post({
      controller: this.options.controller,
      action: this.options.action,
      queryString: this.options.queryString,
      headers: new HttpHeaders({"responseType": "blob"})
    },fileData).subscribe(data=>{

      const message: string="Dosyalar başarılı bir şekilde yüklendi."

      if(this.options.isAdminPage){
        this.alertifyService.message(message,{
          dismissOthers: true,
          messageType: MessageType.Success,
          position: Position.BottomCenter
        })
      }
      else{
        this.customToastrService.message(message,"Başarılı",{
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.BottomCenter
        })
      }

    },(errorResponse: HttpErrorResponse)=>{

      const message:string="Dosyalar yüklenirken bir hatayla karşılaşıldı.";

      if(this.options.isAdminPage){
        this.alertifyService.message(message,{
          dismissOthers: true,
          messageType: MessageType.Error,
          position: Position.BottomCenter
        })
      }
      else{
        this.customToastrService.message(message,"Başarısız",{
          messageType: ToastrMessageType.Error,
          position: ToastrPosition.BottomCenter
        })
      }

    });
  }

}

export class FileUploadOptions{
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}
