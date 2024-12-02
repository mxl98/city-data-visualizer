import { Component, inject } from '@angular/core';
import { ApiService } from '../api.service';
import { PiscineDataComponent } from '../piscine-data/piscine-data.component';
import { Piscine } from '../piscine';
import { CommonModule } from '@angular/common';
import { ArrondissementDropdownComponent } from '../arrondissement-dropdown/arrondissement-dropdown.component';

@Component({
  selector: 'app-body',
  standalone: true,
  imports: [
    CommonModule, 
    ArrondissementDropdownComponent,
    PiscineDataComponent
  ],
  templateUrl: './body.component.html',
  styleUrl: './body.component.scss'
})

export class BodyComponent {
  piscineList: Piscine[] = [];
  allPiscinesList: Piscine[] = [];
  selectedFilters: string[] = [];
  apiService: ApiService = inject(ApiService);

  constructor() {
     this.apiService.getAllPiscine().then((piscineList: Piscine[]) => 
       {
         this.piscineList = piscineList;
         this.allPiscinesList = piscineList;
       });
  }

  onFiltersApplied(filters: string[]): void {
    this.updateList(filters);
  }

  updateList(filters: string[]): void {
    if (filters.length != 0) {
      this.piscineList = this.allPiscinesList.filter((piscine) =>
        filters.includes(piscine.arrondisse)
      );
    } else {
      this.piscineList = this.allPiscinesList;
    }
  }
}
