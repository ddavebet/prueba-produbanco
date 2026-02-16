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
import { ProductoDto } from '../../../../core/models/producto.model';
import { CrearTransaccionDto } from '../../../../core/models/transaccion.model';
import { ProductoService } from '../../../../core/service/producto.service';
import { TransaccionService } from '../../../../core/service/transaccion.service';

@Component({
  selector: 'app-transaccion-registrar-page',
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
  templateUrl: './transaccion-registrar-page.component.html',
})
export class TransaccionRegistrarPageComponent {
  private messageService = inject(MessageService);
  private transaccionService = inject(TransaccionService);
  private productoService = inject(ProductoService);
  private fb = inject(FormBuilder);

  productos: ProductoDto[] = [];
  registrarForm: FormGroup;

  constructor() {
    this.productoService.obtener().subscribe({
      next: (result) => {
        this.productos = result.productos ?? [];
      },
    });

    this.registrarForm = this.fb.group({
      tipo: [null, Validators.required],
      productoId: [null, Validators.required],
      cantidad: [null, [Validators.required, Validators.min(1)]],
      detalle: [null],
    });
  }

  registrar() {
    if (this.registrarForm.valid) {
      const nuevaTransaccion: CrearTransaccionDto = this.registrarForm.value;

      this.registrarForm.disable();

      this.transaccionService.crear(nuevaTransaccion).subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Info',
            detail: 'Transacción creada exitosamente',
            life: 3000,
          });
          this.registrarForm.reset();
        },
        error: (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: error.error.detail,
            life: 3000,
          });
          console.log(error);
          this.registrarForm.enable();
        },
      });
    }
  }
}
