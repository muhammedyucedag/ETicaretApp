import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { User } from 'src/app/entities/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
[x: string]: any;

  constructor(private formBuilder: FormBuilder)
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
  onSubmit(data:User){
    this.submitted = true;

    if(this.registerFormGroup.invalid)
    return;
  }

}
