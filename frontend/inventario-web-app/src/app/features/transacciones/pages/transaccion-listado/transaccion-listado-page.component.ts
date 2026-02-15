import { CommonModule, DatePipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { TableModule } from 'primeng/table';
import { TransaccionService } from '../../services/transaccion.service';
import { TransaccionDto } from '../../../../core/models/transaccion.model';

@Component({
  selector: 'app-transaccion-listado-page',
  standalone: true,
  imports: [CommonModule, DatePipe, TableModule],
  templateUrl: './transaccion-listado-page.component.html',
})
export class TransaccionListadoPageComponent implements OnInit, OnDestroy {
  private transaccionService = inject(TransaccionService);

  cargando = false;
  transaccionesSub!: Subscription;
  transacciones: TransaccionDto[] = [];

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
      .obtener(undefined, undefined, 1, undefined, undefined, undefined)
      .subscribe((result) => {
        this.transaccionService.transacciones.next(result.transacciones ?? []);
        this.cargando = false;
      });
  }
}
