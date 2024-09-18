import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Forecast } from './forecast';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = "http://localhost:8080/weatherforecast";

  values: Forecast[] = [
    {
      date: "2024-09-11",
      temperatureC: 30,
      temperatureF: 90,
      summary: 'Hot'
    },
    {
      date: "2024-09-12",
      temperatureC: 35,
      temperatureF: 100,
      summary: 'Scorching'
    }
  ];
  
  constructor(private http: HttpClient) { }

  async getAllForecasts(): Promise<Forecast[]> {
    const data = await fetch(this.apiUrl);
    return (await data.json()) ?? [];
  }

  getDummyForecast(): Forecast[] {
    return this.values;
  }
}
