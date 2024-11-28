import { inject, Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DropdownService {
  apiService: ApiService = inject(ApiService);

  constructor() {}

  resolve(): Observable<string[]> {
    return this.apiService.getAllArrondissements();
  }
}
