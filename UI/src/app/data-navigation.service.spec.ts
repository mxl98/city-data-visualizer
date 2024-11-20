import { TestBed } from '@angular/core/testing';

import { DataNavigationService } from './data-navigation.service';

describe('DataNavigationService', () => {
  let service: DataNavigationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataNavigationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
