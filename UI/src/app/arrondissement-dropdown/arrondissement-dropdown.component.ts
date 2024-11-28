import { Component, inject, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { CommonModule } from '@angular/common';

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
  apiService: ApiService = inject(ApiService);

  constructor() {}

  ngOnInit(): void {
    this.loadDropdownOptions();
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
}
