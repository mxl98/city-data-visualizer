import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataNavigationService {
  displayDetails(id: number): void {
    document.getElementById('data-details-' + id)?.classList.remove('hide');
  }

  hideDetails(id: number): void {
    document.getElementById('data-details-' + id)?.classList.add('hide');
  }

  displaySummary(id: number): void {
    document.getElementById('data-summary-' + id)?.classList.remove('hide');
  }

  hideSummary(id: number): void {
    document.getElementById('data-summary-' + id)?.classList.add('hide');
  }

  testFunction(id: number): void {
    let element = document.getElementById('data-summary-' + id)?.textContent;
    console.log(element);
  }
}
