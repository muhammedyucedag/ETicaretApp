import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { UserService } from 'src/app/services/common/models/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
[x: string]: any;

  constructor(private formBuilder: FormBuilder, private userService: UserService, private toastrService:CustomToastrService)
  {
  }

  registerFormGroup: FormGroup;

  ngOnInit(): void{
    this.registerFormGroup= this.formBuilder.group({
      nameSurname:["",[
        Validators.required, 
        Validators.maxLength(50),
        Validators.minLength(3)
      ]], 
      username:["",[
        Validators.required, 
        Validators.maxLength(50),
        Validators.minLength(3)
      ]],
      email:["",[
        Validators.required, 
        Validators.maxLength(250),
        Validators.email
      ]],
      password:["",
        Validators.required],
      passwordRepeat:["",
        Validators.required],   
    },{validators: (group: AbstractControl) : ValidationErrors | null=>{
      let password = group.get("password").value;
      let passwordRepeat = group.get("passwordRepeat").value
      return password === passwordRepeat ? null : {notSame:true};
    }})
  }

  get component(){
    return this.registerFormGroup.controls;
  }

  submitted: boolean = false;
  async onSubmit(user:User){
    this.submitted = true;

    if(this.registerFormGroup.invalid)
    return;

    const result: Create_User = await this.userService.create(user);
    if(result.succeeded)
      this.toastrService.message(result.message,"Kullanıcı Kaydı Başarılı",{
        messageType: ToastrMessageType.Success,
        position:ToastrPosition.BottomFullWidth
      })
    else 
    this.toastrService.message(result.message,"Hata",{
      messageType: ToastrMessageType.Error,
      position:ToastrPosition.BottomFullWidth
    })
  }

}
