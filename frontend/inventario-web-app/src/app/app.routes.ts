import { Routes } from '@angular/router';
import { InicioComponent } from './features/inicio/inicio.component';
import { ProductoCrearPageComponent } from './features/productos/pages/producto-crear/producto-crear-page.component';
import { ProductoListadoPageComponent } from './features/productos/pages/producto-listado/producto-listado-page.component';
import { ProductoModificarPageComponent } from './features/productos/pages/producto-modificar/producto-modificar-page.component';
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
      { path: 'productos', component: ProductoListadoPageComponent },
      { path: 'productos/crear', component: ProductoCrearPageComponent },
      {
        path: 'productos/modificar',
        component: ProductoModificarPageComponent,
      },
    ],
  },
];
