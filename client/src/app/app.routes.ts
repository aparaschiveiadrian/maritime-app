import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component'

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'ship', component: HomeComponent},
  {path: 'country', component: HomeComponent},
  {path: 'port', component: HomeComponent},
  {path: 'voyage', component: HomeComponent}
];
