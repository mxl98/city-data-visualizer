import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PiscineDataComponent } from './piscine-data.component';

describe('PiscineDataComponent', () => {
  let component: PiscineDataComponent;
  let fixture: ComponentFixture<PiscineDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PiscineDataComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PiscineDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
