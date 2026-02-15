export interface CrearTransaccionDto {
  tipo?: number;
  productoId?: string;
  cantidad?: number;
  precioUnitario?: number;
  detalle?: string | undefined;
}

export interface TransaccionDto {
  id?: string;
  fecha?: Date;
  tipo?: string;
  productoId?: string;
  productoNombre?: string;
  productoStock?: number;
  cantidad?: number;
  precioUnitario?: number;
  precioTotal?: number;
  detalle?: string | undefined;
}

export interface TransaccionResultadoDto {
  transacciones?: TransaccionDto[];
  total?: number;
  pagina?: number;
  tamano?: number;
}
