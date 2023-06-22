import { ComponentFactoryResolver, Injectable } from '@angular/core';
import { BaseComponent } from 'src/app/base/base.component';

@Injectable({
  providedIn: 'root'
})
export class DynmaicLoadComponentService {

  constructor(private componentFactoryResolver : ComponentFactoryResolver) { }

  async loadComponent(component:Component){
    let _component : any = null;

    switch  (component){
      case Component.BasketsComponent:
        _component = await import("../../ui/components/baskets/baskets.component");
        break;
    }
  }
}

export enum Component {
  BasketsComponent
}
