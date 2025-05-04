import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component'
import { CountryComponent } from './pages/country/country.component';
import { ShipComponent } from './pages/ship/ship.component';
import { PortComponent } from './pages/port/port.component';
import { VoyageComponent } from './pages/voyage/voyage.component';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'ship', component: ShipComponent},
  {path: 'country', component: CountryComponent},
  {path: 'port', component: PortComponent},
  {path: 'voyage', component: VoyageComponent}
];
