import { NgModule } from '@angular/core';
import { DeleteDirective} from './delete.directive';



@NgModule({
  declarations: [DeleteDirective],
  exports: [DeleteDirective]
})
export class DeleteDirectiveModule { }