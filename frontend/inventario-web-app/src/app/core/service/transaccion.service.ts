import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment as env } from '../../../environments/environment';
import {
  CrearTransaccionDto,
  TransaccionDto,
  TransaccionResultadoDto,
} from '../models/transaccion.model';

@Injectable({
  providedIn: 'root',
})
export class TransaccionService {
  private httpClient = inject(HttpClient);
  private apiUrl = `${env.transaccionApiUrl}/api/`;

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
      `${this.apiUrl}transacciones`,
      { params },
    );
  }

  crear(transaccion: CrearTransaccionDto) {
    return this.httpClient.post<TransaccionDto>(
      `${this.apiUrl}transacciones`,
      transaccion,
    );
  }
}
