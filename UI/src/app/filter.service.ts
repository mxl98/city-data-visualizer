import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  handleArrondissements(isActive: boolean): void {
    if (isActive) {
      document.getElementById("arrondissements-dropdown")?.classList.remove("hide");
    } else {
      document.getElementById("arrondissements-dropdown")?.classList.add("hide");
    }
  }

  applyFilter(filters: string[]) {
    console.log(filters);
  }
}
