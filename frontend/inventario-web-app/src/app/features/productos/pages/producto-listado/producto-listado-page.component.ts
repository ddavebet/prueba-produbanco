import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { TooltipModule } from 'primeng/tooltip';
import { Subscription } from 'rxjs';
import { ProductoDto } from '../../../../core/models/producto.model';
import { ProductoService } from '../../../../core/service/producto.service';

@Component({
  selector: 'app-producto-listado-page',
  standalone: true,
  imports: [
    ButtonModule,
    CommonModule,
    RouterModule,
    TableModule,
    TooltipModule,
  ],
  templateUrl: './producto-listado-page.component.html',
})
export class ProductoListadoPageComponent implements OnInit, OnDestroy {
  private productoService = inject(ProductoService);
  private router = inject(Router);

  cargando = false;
  productosSub!: Subscription;
  productos: ProductoDto[] = [];

  ngOnInit() {
    this.productos = [];
    this.productosSub = this.productoService.productos.subscribe(
      (productos) => {
        this.productos = productos;
      },
    );
    this.cargar();
  }

  ngOnDestroy(): void {
    this.productosSub.unsubscribe();
  }

  cargar() {
    this.cargando = true;
    this.productoService.productos.next([]);
    this.productoService
      .obtener(undefined, undefined, undefined, undefined, undefined, undefined)
      .subscribe((result) => {
        this.productoService.productos.next(result.productos ?? []);
        this.cargando = false;
      });
  }

  navegarModificar(id: string) {
    this.productoService.idSeleccionado.next(id);
    this.router.navigate(['/productos/modificar']);
  }
}
