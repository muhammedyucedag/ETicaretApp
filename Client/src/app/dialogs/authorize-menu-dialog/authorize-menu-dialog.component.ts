import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialog';
import { RoleService } from 'src/app/services/common/models/role.service';
import { list_role } from 'src/app/contracts/role/list_role';
import { MatSelectionList } from '@angular/material/list';

@Component({
  selector: 'app-authorize-menu-dialog',
  templateUrl: './authorize-menu-dialog.component.html',
  styleUrls: ['./authorize-menu-dialog.component.css']
})
export class AuthorizeMenuDialogComponent extends BaseDialog<AuthorizeMenuDialogComponent> implements OnInit{
  constructor(dialogRef: MatDialogRef<AuthorizeMenuDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private roleService: RoleService){ 
    super(dialogRef)
  }
  roles: {datas: list_role[], totalCount: number };

  async ngOnInit() {
    this.roles = await this.roleService.getRoles(-1, -1);
  }

  assignRoles(rolesComponent: MatSelectionList){
    const roles : string[] = rolesComponent.selectedOptions.selected.map(o => o._text.nativeElement.innertText);
  }

}

export enum AuthorizeMenuState{
  Yes,
  No
}
