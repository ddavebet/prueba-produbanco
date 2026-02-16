import { Component, inject } from '@angular/core';
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
import { SelectModule } from 'primeng/select';
import { ToastModule } from 'primeng/toast';
import { CrearProductoDto } from '../../../../core/models/producto.model';
import { ProductoService } from '../../../../core/service/producto.service';

@Component({
  selector: 'app-producto-crear-page',
  standalone: true,
  imports: [
    ButtonModule,
    InputNumberModule,
    InputTextModule,
    RadioButtonModule,
    ReactiveFormsModule,
    SelectModule,
    ToastModule,
  ],
  providers: [MessageService],
  templateUrl: './producto-crear-page.component.html',
})
export class ProductoCrearPageComponent {
  private messageService = inject(MessageService);
  private productoService = inject(ProductoService);
  private fb = inject(FormBuilder);

  crearForm: FormGroup;

  constructor() {
    this.crearForm = this.fb.group({
      nombre: [null, Validators.required],
      descripcion: [null],
      categoria: [null, Validators.required],
      precio: [null, [Validators.required, Validators.min(0.01)]],
      stockInicial: [null, [Validators.required, Validators.min(1)]],
    });
  }

  crear() {
    if (this.crearForm.valid) {
      const nuevoProducto: CrearProductoDto = this.crearForm.value;

      this.crearForm.disable();

      this.productoService.crear(nuevoProducto).subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Info',
            detail: 'Producto creado exitosamente',
            life: 3000,
          });
          this.crearForm.reset();
        },
        error: (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: error.error.detail,
            life: 3000,
          });
          console.log(error);
          this.crearForm.enable();
        },
      });
    }
  }
}
