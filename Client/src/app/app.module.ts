import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BaseComponent } from './base/base.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { DeleteDialogComponent } from './dialogs/delete-dialog/delete-dialog.component';
import { FileUploadComponent } from './services/common/file-upload/file-upload.component';
import { FileUploadDialogComponent } from './dialogs/file-upload-dialog/file-upload-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './ui/components/login/login.component';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { HttpErrorHandlerInterceptorService } from './services/common/http-error-handler-interceptor.service';
import { DynamicLoadComponentDirective } from './directives/common/dynamic-load-component.directive';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DynamicLoadComponentDirective
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AdminModule,UiModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    HttpClientModule,
    MatDialogModule,
    JwtModule.forRoot({
      config:{
        tokenGetter: () => localStorage.getItem("accessToken"),
        allowedDomains: ["localhost:7164"]
      }
    }),
    SocialLoginModule
  ],
  providers: [
    {provide:"baseUrl", useValue:"https://localhost:7164/api", multi: true},
    {provide:"baseSignalRUrl", useValue:"https://localhost:7164/", multi: true},
      {
        provide: "SocialAuthServiceConfig",
        useValue: {
          autoLogin: false,
          providers: [
            {
              id: GoogleLoginProvider.PROVIDER_ID,
              provider: new GoogleLoginProvider("467572750567-o0k7f3vn1lo640j2abihr7go30qndf7l.apps.googleusercontent.com")
            },
            {
              id: FacebookLoginProvider.PROVIDER_ID,
              provider: new FacebookLoginProvider("233715776046151")
            }
          ],
          onError: err => console.log(err)
        } as SocialAuthServiceConfig
      },
      {provide:HTTP_INTERCEPTORS, useClass:HttpErrorHandlerInterceptorService, multi: true}
  ],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
