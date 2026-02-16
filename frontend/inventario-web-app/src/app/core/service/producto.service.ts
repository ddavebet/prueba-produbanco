import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment as env } from '../../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import {
  CrearProductoDto,
  ProductoDto,
  ProductoResultadoDto,
} from '../models/producto.model';

@Injectable({
  providedIn: 'root',
})
export class ProductoService {
  private httpClient = inject(HttpClient);
  private apiUrl = `${env.productoApiUrl}/api/`;

  productos = new BehaviorSubject<ProductoDto[]>([]);

  obtener(
    nombre: string | undefined,
    categoria: number | undefined,
    precioMin: number | undefined,
    precioMax: number | undefined,
    pagina: number | undefined,
    tamano: number | undefined,
  ) {
    let params = new HttpParams();

    if (nombre) {
      params = params.set('nombre', nombre);
    }
    if (categoria !== undefined) {
      params = params.set('categoria', categoria.toString());
    }
    if (precioMin !== undefined) {
      params = params.set('precioMin', precioMin.toString());
    }
    if (precioMax !== undefined) {
      params = params.set('precioMax', precioMax.toString());
    }
    if (pagina !== undefined) {
      params = params.set('pagina', pagina.toString());
    }
    if (tamano !== undefined) {
      params = params.set('tamano', tamano.toString());
    }

    return this.httpClient.get<ProductoResultadoDto>(
      `${this.apiUrl}productos`,
      { params },
    );
  }

  obtenerPorId(id: string) {}

  crear(producto: CrearProductoDto) {
    return this.httpClient.post<ProductoDto>(
      `${this.apiUrl}productos`,
      producto,
    );
  }

  actualizar() {}

  eliminar() {}
}
