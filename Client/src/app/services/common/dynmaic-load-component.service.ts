import { ComponentFactoryResolver, Injectable, ViewContainerRef } from '@angular/core';
import { BaseComponent } from 'src/app/base/base.component';

@Injectable({
  providedIn: 'root'
})
export class DynmaicLoadComponentService {

  //ViewContainerRef = Dinamik olarak yüklenecek componenti içerisinde barındıran container'dır (Her dinamik yükleme sürecinde önceki view'leri clear etmemiz gerekmektedir.)
  //ComponentFactory = Component'lerin instance'larını oluşturmak için kullanılan nesnedir.
  //ComponentFactoryResolver = Belirli bir component için ComponentFactory'i rezolve eden sınıftır. İçerisindeki resolveComponentFactory fonskiyonu aracılığıyla ilgili componente dair bir ComponentFactory nesnesi olutşrup döner

  constructor() { }

  async loadComponent(component:ComponentType, viewContainerRef : ViewContainerRef){
    let _component : any = null;

    switch  (component){
      case ComponentType.BasketsComponent:
        _component = (await import("../../ui/components/baskets/baskets.component")).BasketsComponent;
        break;
    }

    viewContainerRef.clear();
    return viewContainerRef.createComponent(_component)
  }
}

export enum ComponentType {
  BasketsComponent
}
