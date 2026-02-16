import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { Subscription } from 'rxjs';
import { ProductoDto } from '../../../../core/models/producto.model';
import { ProductoService } from '../../../../core/service/producto.service';

@Component({
  selector: 'app-producto-listado-page',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, RouterModule],
  templateUrl: './producto-listado-page.component.html',
})
export class ProductoListadoPageComponent implements OnInit, OnDestroy {
  private productoService = inject(ProductoService);

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
}
