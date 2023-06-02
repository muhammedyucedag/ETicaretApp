import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectProductImageDialogComponent } from './select-product-image-dialog.component';

describe('SelectProductImageDialogComponent', () => {
  let component: SelectProductImageDialogComponent;
  let fixture: ComponentFixture<SelectProductImageDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectProductImageDialogComponent]
    });
    fixture = TestBed.createComponent(SelectProductImageDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
