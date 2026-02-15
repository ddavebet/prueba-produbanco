import { Routes } from '@angular/router';
import { TransaccionPageComponent } from './features/transacciones/pages/transaccion-page.component';
import { InicioComponent } from './features/inicio/inicio.component';

export const routes: Routes = [
  {
    path: '',
    component: InicioComponent,
    children: [
      { path: '', component: TransaccionPageComponent },
      { path: 'transacciones', component: TransaccionPageComponent },
    ],
  },
];
