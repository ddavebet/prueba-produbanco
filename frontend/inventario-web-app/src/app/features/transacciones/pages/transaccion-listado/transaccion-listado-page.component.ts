import { CommonModule, DatePipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { TableModule } from 'primeng/table';
import { Subscription } from 'rxjs';
import { TransaccionDto } from '../../../../core/models/transaccion.model';
import { TransaccionService } from '../../../../core/service/transaccion.service';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ProductoService } from '../../../../core/service/producto.service';
import { ProductoDto } from '../../../../core/models/producto.model';
import { SelectModule } from 'primeng/select';

@Component({
  selector: 'app-transaccion-listado-page',
  standalone: true,
  imports: [
    ButtonModule,
    CommonModule,
    DatePickerModule,
    DatePipe,
    FormsModule,
    RouterModule,
    TableModule,
    RadioButtonModule,
    SelectModule,
  ],
  templateUrl: './transaccion-listado-page.component.html',
})
export class TransaccionListadoPageComponent implements OnInit, OnDestroy {
  private transaccionService = inject(TransaccionService);
  private productoService = inject(ProductoService);

  cargando = false;
  transaccionesSub!: Subscription;
  transacciones: TransaccionDto[] = [];
  filtros = {
    fechaInicio: undefined,
    fechaFin: undefined,
    tipo: undefined,
    productoId: undefined,
  };
  productos: ProductoDto[] = [];

  constructor() {
    this.productoService.obtener().subscribe({
      next: (result) => {
        this.productos = result.productos ?? [];
      },
    });
  }

  ngOnInit() {
    this.transacciones = [];
    this.transaccionesSub = this.transaccionService.transacciones.subscribe(
      (transacciones) => {
        this.transacciones = transacciones;
      },
    );
    this.cargar();
  }

  ngOnDestroy(): void {
    this.transaccionesSub.unsubscribe();
  }

  cargar() {
    this.cargando = true;
    this.transaccionService.transacciones.next([]);
    this.transaccionService
      .obtener(
        this.filtros.fechaInicio,
        this.filtros.fechaFin,
        this.filtros.tipo,
        this.filtros.productoId,
        undefined,
        undefined,
      )
      .subscribe((result) => {
        this.transaccionService.transacciones.next(result.transacciones ?? []);
        this.cargando = false;
      });
  }
}
