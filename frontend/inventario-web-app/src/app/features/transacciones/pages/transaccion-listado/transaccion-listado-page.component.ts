import { CommonModule, DatePipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { Subscription } from 'rxjs';
import { TransaccionDto } from '../../../../core/models/transaccion.model';
import { TransaccionService } from '../../services/transaccion.service';

@Component({
  selector: 'app-transaccion-listado-page',
  standalone: true,
  imports: [CommonModule, DatePipe, TableModule, ButtonModule, RouterModule],
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
      .obtener(undefined, undefined, undefined, undefined, undefined, undefined)
      .subscribe((result) => {
        this.transaccionService.transacciones.next(result.transacciones ?? []);
        this.cargando = false;
      });
  }
}
