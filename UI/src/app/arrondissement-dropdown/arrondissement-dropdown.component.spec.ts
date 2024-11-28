import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArrondissementDropdownComponent } from './arrondissement-dropdown.component';

describe('ArrondissementDropdownComponent', () => {
  let component: ArrondissementDropdownComponent;
  let fixture: ComponentFixture<ArrondissementDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArrondissementDropdownComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ArrondissementDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
