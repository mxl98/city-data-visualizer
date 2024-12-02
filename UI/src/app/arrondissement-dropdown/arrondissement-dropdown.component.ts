import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { ApiService } from '../api.service';
import { CommonModule } from '@angular/common';
import { FilterService } from '../filter.service';
import { FormGroup, FormControl, ReactiveFormsModule, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-arrondissement-dropdown',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './arrondissement-dropdown.component.html',
  styleUrl: './arrondissement-dropdown.component.scss'
})

export class ArrondissementDropdownComponent implements OnInit {
  @Output() filtersApplied = new EventEmitter<string[]>();
  dropdownOptions: string[] = [];
  isActive: boolean = false;
  apiService: ApiService = inject(ApiService);
  filterService: FilterService = inject(FilterService);
  filterForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loadDropdownOptions();
    if (this.dropdownOptions.length == 0) {
      this.dropdownOptions = ["arr1", "arr2", "arr3", "arr4", "arr5"];
    }
    this.createForm();
  }

  createForm(): void {
    const formControls: { [key: string]: FormControl } = {};

    this.dropdownOptions.forEach((option) => {
      formControls[option] = new FormControl(false);
    });

    this.filterForm = this.fb.group(formControls);
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
  }

  onSubmit(): void {
    const selectedFilters = Object.keys(this.filterForm.value).filter(
      (key) => this.filterForm.value[key] === true);
    this.filtersApplied.emit(selectedFilters);
  }
}
