import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasketShoppingComploteDialogComponent } from './basket-shopping-complote-dialog.component';

describe('BasketShoppingComploteDialogComponent', () => {
  let component: BasketShoppingComploteDialogComponent;
  let fixture: ComponentFixture<BasketShoppingComploteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasketShoppingComploteDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BasketShoppingComploteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
