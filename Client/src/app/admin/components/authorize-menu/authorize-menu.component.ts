import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from 'src/app/base/base.component';
import { Menu } from 'src/app/contracts/application-configurations/menu';
import { AuthorizeMenuDialogComponent } from 'src/app/dialogs/authorize-menu-dialog/authorize-menu-dialog.component';
import { DialogService } from 'src/app/services/common/dialog.service';
import { ApplicationService } from 'src/app/services/common/models/application.service';

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

interface FoodNode {
  name: string;
  children?: FoodNode[];
}

interface ITreeMenu {
      name?: string,
      actions?: ITreeMenu[],
      code?: string
}

@Component({
  selector: 'app-authorize-menu',
  templateUrl: './authorize-menu.component.html',
  styleUrls: ['./authorize-menu.component.css']
})
export class AuthorizeMenuComponent extends BaseComponent implements OnInit{

  constructor(spinner: NgxSpinnerService, private applicationService: ApplicationService, private dialogService: DialogService) {
    super(spinner)
  }

  async ngOnInit(){
    this.dataSource.data = (await this.applicationService.getAuthorizeDefinitionEndPoints()).map(m => {
      const treeMenu : ITreeMenu = {
        name : m.name,
        actions : m.actions.map(a => {
          const _treeMenu: ITreeMenu = {
            name: a.definition,
            code: a.code
          }
          return _treeMenu;
        })
      };
      return treeMenu;
    });
  }

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable,
  );

  treeFlattener = new MatTreeFlattener(
    (menu: ITreeMenu, level: number) => {
      return {
        expandable: menu.actions?.length > 0,
        name: menu.name,
        level: level,
        code : menu.code
      };
    },
    menu => menu.level,
    menu => menu.expandable,
    menu => menu.actions
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);


  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  assingRole(code : string, name: string){
    this.dialogService.openDialog({
      componentType: AuthorizeMenuDialogComponent,
      data: {code,name},
      options: {
        width: "750px"
      },
      afterClosed: () => {

      }
    })
  }
}

