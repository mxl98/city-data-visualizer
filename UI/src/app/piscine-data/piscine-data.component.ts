import { Component, Input } from '@angular/core';
import { Piscine } from '../piscine';
import { DataNavigationService } from '../data-navigation.service';

@Component({
  selector: 'app-piscine-data',
  standalone: true,
  imports: [],
  templateUrl: './piscine-data.component.html',
  styleUrl: './piscine-data.component.scss'
})
export class PiscineDataComponent {
  @Input() piscine!: Piscine;

  constructor(private _dataNavigationService: DataNavigationService) {}

  onClickDetails(id: number): void {
    this._dataNavigationService.hideSummary(id);
    this._dataNavigationService.displayDetails(id);
  }

  onClickSummary(id: number): void {
    this._dataNavigationService.hideDetails(id);
    this._dataNavigationService.displaySummary(id);
  }

  testFunction(id: number): void {
    this._dataNavigationService.testFunction(id);
  }
}
