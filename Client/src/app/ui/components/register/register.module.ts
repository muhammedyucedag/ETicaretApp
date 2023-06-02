import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path:"",component:RegisterComponent}
    ]),
    ReactiveFormsModule // Reaktif form, kullanıcı etkileşimlerine anında tepki verebilen ve dinamik olarak değişebilen web formları oluşturmak için kullanılan bir yaklaşımdır.
  ]
})
export class RegisterModule { }
