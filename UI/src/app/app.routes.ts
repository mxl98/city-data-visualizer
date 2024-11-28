import { Routes } from '@angular/router';
import { ArrondissementDropdownComponent } from './arrondissement-dropdown/arrondissement-dropdown.component';
import { DropdownService } from './resolvers/dropdown.service';

export const routes: Routes = [
    {
        path: '',
        component: ArrondissementDropdownComponent,
        resolve: { dropdownOptions: DropdownService }
    }
];
