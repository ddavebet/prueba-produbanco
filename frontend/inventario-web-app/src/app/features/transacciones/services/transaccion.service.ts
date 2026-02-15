import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import {
  CrearTransaccionDto,
  TransaccionDto,
  TransaccionResultadoDto,
} from '../../../core/models/transaccion.model';

@Injectable({
  providedIn: 'root',
})
export class TransaccionService {
  private httpClient = inject(HttpClient);
  private baseUrl = 'https://localhost:7015/api/';

  transacciones = new BehaviorSubject<TransaccionDto[]>([]);

  obtener(
    fechaInicio: Date | undefined,
    fechaFin: Date | undefined,
    tipo: number | undefined,
    productoId: string | undefined,
    pagina: number | undefined,
    tamano: number | undefined,
  ) {
    let params = new HttpParams();

    if (fechaInicio) {
      params = params.set('fechaInicio', fechaInicio.toISOString());
    }
    if (fechaFin) {
      params = params.set('fechaFin', fechaFin.toISOString());
    }
    if (tipo !== undefined) {
      params = params.set('tipo', tipo.toString());
    }
    if (productoId) {
      params = params.set('productoId', productoId);
    }
    if (pagina !== undefined) {
      params = params.set('pagina', pagina.toString());
    }
    if (tamano !== undefined) {
      params = params.set('tamano', tamano.toString());
    }

    return this.httpClient.get<TransaccionResultadoDto>(
      `${this.baseUrl}transacciones`,
      { params },
    );
  }

  crear(transaccion: CrearTransaccionDto) {}
}
