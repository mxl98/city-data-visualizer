import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Piscine } from './piscine';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private piscineUrl = "http://localhost:8080/api/piscines";

  private piscineList: Piscine[] = [
    {
      iD_UEV: 0,
      type: 'Piscine intérieure',
      nom: 'Complexe Sportif ABC',
      arrondisse: 'Ahuntsic-Cartierville',
      adresse: '1 000, Avenue Émile-Journeault Est',
      propriete: 'Municipale',
      gestion: 'Municipale',
      poinT_X: '294151,2717',
      poinT_Y: '5045855457',
      equipeme: 'Complexe aquatique',
      long: -73.63639,
      lat: 45.552526
    },
    {
      iD_UEV: 1,
      type: 'Pataugeoire',
      nom: 'Parc Saint-Jean-Baptiste',
      arrondisse: 'Rivière-des-Prairies–Pointe-aux-Trembles',
      adresse: '1 048, Boulevard Saint-Jean-Baptiste',
      propriete: 'Municipale',
      gestion: 'Municipale',
      poinT_X: '304846,2071',
      poinT_Y: '5055625,72',
      equipeme: '',
      long: -73.49941,
      lat: 45.640521
    }
  ];
  
  constructor(private http: HttpClient) { }

  getAllPiscinesTest(): Piscine[] {
    return this.piscineList;
  }

  async getAllPiscine(): Promise<Piscine[]> {
    const data = await fetch(this.piscineUrl);
    return (await data.json()) ?? [];
  }
}
