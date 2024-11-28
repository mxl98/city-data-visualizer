import { Component, inject, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { CommonModule } from '@angular/common';
import { FilterService } from '../filter.service';

@Component({
  selector: 'app-arrondissement-dropdown',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './arrondissement-dropdown.component.html',
  styleUrl: './arrondissement-dropdown.component.scss'
})

export class ArrondissementDropdownComponent implements OnInit {
  dropdownOptions: string[] = [];
  isActive: boolean = false;
  apiService: ApiService = inject(ApiService);
  filterService: FilterService = inject(FilterService);

  constructor() {}

  ngOnInit(): void {
    this.loadDropdownOptions();
    if (this.dropdownOptions.length == 0) {
      this.dropdownOptions = ["arr1", "arr2", "arr3", "arr4", "arr5"];
    }
  }

  loadDropdownOptions(): void {
    this.apiService.getAllArrondissements()
      .subscribe({
        next: (data) => {
          this.dropdownOptions = data;
        },
        error: (error) => {
          console.error('Failed to load dropdown options', error)
        }
      });
  }

  onClickArrondissements(): void {
    this.isActive = !this.isActive;
    this.filterService.handleArrondissements(this.isActive);
    console.log(this.dropdownOptions);
  }
}
