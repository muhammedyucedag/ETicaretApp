import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasketsComponent } from './baskets.component';

describe('BasketsComponent', () => {
  let component: BasketsComponent;
  let fixture: ComponentFixture<BasketsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BasketsComponent]
    });
    fixture = TestBed.createComponent(BasketsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
