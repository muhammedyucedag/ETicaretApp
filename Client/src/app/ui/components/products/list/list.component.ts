import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { List_Product } from 'src/app/contracts/list_product';
import { ProductService } from 'src/app/services/common/models/product.service';
import { FileService } from '../../../../services/common/models/file.service';
import { BaseUrl } from 'src/app/contracts/base_url';
import { BasketService } from 'src/app/services/common/models/basket.service';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { Create_Basket_Item } from 'src/app/contracts/basket/create_basket_item';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent extends BaseComponent implements OnInit {

  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute, private fileService: FileService, private basketService: BasketService, spinner: NgxSpinnerService, private customToastrService: CustomToastrService) 
  { 
    super(spinner)
  }

  currentPageNo: number;
  totalProductCount: number;
  totalPageCount: number;
  pageSize: number = 12;
  pageList: number[] = [];
  baseUrl: BaseUrl;

  products: List_Product[];

    async ngOnInit(){
      
      this.baseUrl = await this.fileService.getBaseStorageUrl();
      
      this.activatedRoute.params.subscribe(async params => {
      this.currentPageNo = parseInt(params["pageNo"] ?? 1);

      const data: {totalProductCount: number, products: List_Product[] } = await this.productService.read(this.currentPageNo - 1 , this.pageSize, 
        () => {
  
        }, 
        errorMessage => {
  
        })
        this.products = data.products;

        this.products = this.products.map<List_Product>(product => {
          const listProduct: List_Product = {
            id : product.id,
            createdDate: product.createdDate,
            imagePath : `${this.fileService.getBaseStorageUrl()}/${product.productImageFiles.length ? product.productImageFiles.find(p=>p.showcase).path : ""}`,
            name : product.name,
            price: product.price,
            stock: product.stock,
            updateDate: product.updateDate,
            productImageFiles: product.productImageFiles
          };
          return listProduct;
        });

        this.totalProductCount = data.totalProductCount;
        this.totalPageCount = Math.ceil(this.totalProductCount / this.pageSize);

        this.pageList = [];

        if (this.totalPageCount >= 7)
        {
              if (this.currentPageNo - 3 <= 0)
              {
                for (let i = 1; i <= 7; i++)
                {
                  this.pageList.push(i);
                }
              }
              else if (this.currentPageNo + 3 >= this.totalPageCount)
              {
                for (let i = this.totalPageCount - 6; i <= this.totalPageCount; i++)
                {
                  this.pageList.push(i);
                }
              }
              else
              {
                for (let i = this.currentPageNo - 3; i <= this.currentPageNo + 3; i++)
                {
                  this.pageList.push(i);
                }
              }
        }
        else
        {
              for (let i = 1; i <= this.totalPageCount; i++)
              {
                this.pageList.push(i);
              }
        }
    });  
  }

  async addToBasket(product: List_Product) {
    this.showSpinner(SpinnerType.BallAtom);
    let _basketItem: Create_Basket_Item = new Create_Basket_Item();
    _basketItem.productId = product.id;
    _basketItem.quantity = 1;
    await this.basketService.add(_basketItem);
    this.hideSpinner(SpinnerType.BallAtom);
    this.customToastrService.message("Ürün sepete eklenmiştir.", "Sepete Eklendi", {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });
  }
}


