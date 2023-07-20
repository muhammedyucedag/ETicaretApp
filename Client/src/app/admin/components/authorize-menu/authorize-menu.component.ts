import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from 'src/app/base/base.component';
import { Menu } from 'src/app/contracts/application-configurations/menu';
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

const TREE_DATA: FoodNode[] = [
  {
    name: 'Fruit',
    children: [{name: 'Apple'}, {name: 'Banana'}, {name: 'Fruit loops'}],
  },
  {
    name: 'Vegetables',
    children: [
      {
        name: 'Green',
        children: [{name: 'Broccoli'}, {name: 'Brussels sprouts'}],
      },
      {
        name: 'Orange',
        children: [{name: 'Pumpkins'}, {name: 'Carrots'}],
      },
    ],
  },
];

@Component({
  selector: 'app-authorize-menu',
  templateUrl: './authorize-menu.component.html',
  styleUrls: ['./authorize-menu.component.css']
})
export class AuthorizeMenuComponent extends BaseComponent implements OnInit{

  constructor(spinner: NgxSpinnerService, private applicationService: ApplicationService) {
    super(spinner)
  }

  async ngOnInit(){
    this.dataSource.data = await this.applicationService.getAuthorizeDefinitionEndPoints();
  }

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable,
  );

  treeFlattener = new MatTreeFlattener(
    (menu: Menu, level: number) => {
      return {
        expandable: menu.actions?.length > 0,
        name: menu.name,
        level: level,
      };
    },
    menu => menu.level,
    menu => menu.expandable,
    menu => menu.actions.map(a => {
      const menu : Menu = new Menu();
      menu.name = a.definition;
      return menu;
    }),
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);


  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

}

