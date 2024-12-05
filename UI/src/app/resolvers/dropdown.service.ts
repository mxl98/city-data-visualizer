import { inject, Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Observable, tap } from 'rxjs';

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
