import { Routes } from '@angular/router';
import { InicioComponent } from './features/inicio/inicio.component';
import { TransaccionListadoPageComponent } from './features/transacciones/pages/transaccion-listado/transaccion-listado-page.component';
import { TransaccionRegistrarPageComponent } from './features/transacciones/pages/transaccion-registrar/transaccion-registrar-page.component';

export const routes: Routes = [
  {
    path: '',
    component: InicioComponent,
    children: [
      { path: '', component: TransaccionListadoPageComponent },
      { path: 'transacciones', component: TransaccionListadoPageComponent },
      {
        path: 'transacciones/registrar',
        component: TransaccionRegistrarPageComponent,
      },
    ],
  },
];
