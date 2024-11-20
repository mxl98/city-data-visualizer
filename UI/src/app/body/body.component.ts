import { Component, inject } from '@angular/core';
import { ApiService } from '../api.service';
import { PiscineDataComponent } from '../piscine-data/piscine-data.component';
import { Piscine } from '../piscine';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-body',
  standalone: true,
  imports: [CommonModule, PiscineDataComponent],
  templateUrl: './body.component.html',
  styleUrl: './body.component.scss'
})

export class BodyComponent {
  piscineList: Piscine[] = [];
  apiService: ApiService = inject(ApiService);

  constructor() {
    this.apiService.getAllPiscine().then((piscineList: Piscine[]) => 
      {
        this.piscineList = piscineList;
      });
  }
}
