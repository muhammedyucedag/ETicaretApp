import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

declare var $: any;

@Component({
  selector: 'app-basket-shopping-complote-dialog',
  templateUrl: './basket-shopping-complote-dialog.component.html',
  styleUrls: ['./basket-shopping-complote-dialog.component.css']
})
export class BasketShoppingComploteDialogComponent extends BaseDialog<BasketShoppingComploteDialogComponent> implements OnDestroy {

  constructor(dialogRef: MatDialogRef<BasketShoppingComploteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ShoppingComploteState)
  { 
    super(dialogRef)
  }

  show: boolean = false;
  complete(){
    this.show = true
  }

  ngOnDestroy(): void {
    if (!this.show)
      $("#basketModal").modal("show");
  }
}

export enum ShoppingComploteState{
  Yes,
  No
}
