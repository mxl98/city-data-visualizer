import { Component, inject } from '@angular/core';
import { ApiService } from '../api.service';
import { NgFor } from '@angular/common';
import { Forecast } from '../forecast';

@Component({
  selector: 'app-forecast',
  standalone: true,
  imports: [
    NgFor
  ],
  templateUrl: './forecast.component.html',
  styleUrl: './forecast.component.scss'
})
export class ForecastComponent {
  apiService: ApiService = inject(ApiService);
  values: Forecast[] = [];

  constructor() {
    this.apiService.getAllForecasts().then((forecastList: Forecast[]) => 
    {
      this.values = forecastList;
    });
  }
}
