import { Routes } from '@angular/router';
import { TransaccionPageComponent } from './features/transacciones/pages/transaccion-page.component';

export const routes: Routes = [
  {
    path: 'transacciones',
    component: TransaccionPageComponent,
  },
  { path: '', redirectTo: 'transacciones', pathMatch: 'full' },
];
