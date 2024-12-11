import { Component, EventEmitter, inject, isDevMode, OnInit, Output } from '@angular/core';
import { ApiService } from '../api.service';
import { CommonModule } from '@angular/common';
import { FilterService } from '../filter.service';
import { FormGroup, FormControl, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';

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

  constructor(private fb: FormBuilder, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadDropdownOptions();
    this.createForm();
  }

  createForm(): void {
    const formControls: { [key: string]: FormControl } = {};

    this.dropdownOptions.forEach((option) => {
      formControls[option] = new FormControl(false);
    });

    this.filterForm = this.fb.group(formControls);
  }

  toggleSelectionBorder(option: string, isChecked: boolean): void {
    const selectedOption = document.getElementById("option-wrapper-" + option);
    isChecked ? selectedOption?.classList.add('border') : selectedOption?.classList.remove('border');
  }

  loadDropdownOptions(): void {
    this.dropdownOptions = [
      "Le Plateau-Mont-Royal",
      "Le Sud-Ouest",
      "Ville-Marie",
      "Verdun",
      "Mercier–Hochelaga-Maisonneuve",
      "Rosemont–La Petite-Patrie",
      "Villeray–Saint-Michel–Parc-Extension",
      "Anjou",
      "Montréal-Nord",
      "Rivière-des-Prairies–Pointe-aux-Trembles",
      "Saint-Léonard",
      "Côte-des-Neiges–Notre-Dame-de-Grâce",
      "Ahuntsic-Cartierville",
      "Outremont",
      "LaSalle",
      "Saint-Laurent",
      "L'Île-Bizard–Sainte-Geneviève",
      "Lachine",
      "Pierrefonds-Roxborro"
    ];
  }

  toggleDropdown(): void {
    this.isActive = !this.isActive;
    this.filterService.handleArrondissements(this.isActive);
    document.getElementById("dropdown-button")?.setAttribute("aria-expanded", this.isActive + "");

    const arrow = document.getElementById("dropdown-arrow");
    //this.isActive ? arrow?.classList.remove('hide') : arrow?.classList.add('hide');
  }

  onSubmit(): void {
    const selectedFilters = Object.keys(this.filterForm.value).filter(
      (key) => this.filterForm.value[key] === true);
    this.filtersApplied.emit(selectedFilters);
  }
}
