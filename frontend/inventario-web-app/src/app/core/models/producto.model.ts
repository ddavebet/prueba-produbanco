export interface ActualizarProductoDto {
  nombre?: string;
  descripcion?: string | undefined;
  categoria?: number;
  imagen?: string | undefined;
  precio?: number;
}

export interface CrearProductoDto {
  nombre?: string;
  descripcion?: string | undefined;
  categoria?: number;
  imagen?: string | undefined;
  precio?: number;
  stockInicial?: number;
}

export interface ProductoDto {
  id?: string;
  nombre?: string;
  descripcion?: string;
  categoria?: string;
  imagen?: string;
  precio?: number;
  stock?: number;
}

export interface ProductoInfoDto {
  id?: string;
  nombre?: string;
  stock?: number;
}

export interface ProductoResultadoDto {
  productos?: ProductoDto[];
  total?: number;
  pagina?: number;
  tamano?: number;
}
