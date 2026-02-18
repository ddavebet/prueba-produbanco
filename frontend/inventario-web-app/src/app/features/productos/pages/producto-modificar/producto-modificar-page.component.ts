import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { ToastModule } from 'primeng/toast';
import { Subscription } from 'rxjs';
import { ActualizarProductoDto } from '../../../../core/models/producto.model';
import { ProductoService } from '../../../../core/service/producto.service';

@Component({
  selector: 'app-producto-modificar-page',
  standalone: true,
  imports: [
    ButtonModule,
    InputNumberModule,
    InputTextModule,
    RadioButtonModule,
    ReactiveFormsModule,
    ToastModule,
  ],
  providers: [MessageService],
  templateUrl: './producto-modificar-page.component.html',
})
export class ProductoModificarPageComponent implements OnInit, OnDestroy {
  private messageService = inject(MessageService);
  private productoService = inject(ProductoService);
  private fb = inject(FormBuilder);

  modificarForm: FormGroup;
  idSeleccionadoSub!: Subscription;
  idSeleccionado: string | undefined;

  constructor() {
    this.modificarForm = this.fb.group({
      nombre: [null, Validators.required],
      descripcion: [null],
      categoria: [null, Validators.required],
      precio: [null, [Validators.required, Validators.min(0.01)]],
      stockInicial: [null, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {
    this.idSeleccionadoSub = this.productoService.idSeleccionado.subscribe(
      (id) => {
        if (id) {
          this.idSeleccionado = id;
          this.cargar(id);
        }
      },
    );
  }

  ngOnDestroy(): void {
    this.idSeleccionadoSub.unsubscribe();
  }

  cargar(id: string) {
    this.productoService.obtenerPorId(id).subscribe({
      next: (producto) => {
        this.modificarForm.patchValue({
          nombre: producto.nombre,
          descripcion: producto.descripcion,
          categoria: producto.categoriaId,
          precio: producto.precio,
          stockInicial: producto.stock,
        });
      },
      error: (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'No se pudo cargar el producto',
          life: 3000,
        });
      },
    });
  }

  modificar() {
    if (this.modificarForm.valid) {
      const productoModificado: ActualizarProductoDto =
        this.modificarForm.value;

      this.modificarForm.disable();

      this.productoService
        .actualizar(this.idSeleccionado!, productoModificado)
        .subscribe({
          next: () => {
            this.messageService.add({
              severity: 'success',
              summary: 'Info',
              detail: 'Producto modificado exitosamente',
              life: 3000,
            });
            this.modificarForm.reset();
          },
          error: (error) => {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: error.error.detail,
              life: 3000,
            });
            this.modificarForm.enable();
          },
        });
    }
  }
}
