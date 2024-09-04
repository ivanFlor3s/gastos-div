import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthContainerComponent } from './container/auth-container/auth-container.component';
import { RegisterComponent, LoginComponent } from './components';
import { AuthRoutes } from './auth.routing';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorsModule } from '../../../core/errors/errors.module';
import { GoogleButtonComponent } from '@app/shared/standalone';

@NgModule({
    declarations: [AuthContainerComponent, RegisterComponent, LoginComponent],
    imports: [
        CommonModule,
        AuthRoutes,
        GoogleButtonComponent,
        ReactiveFormsModule,
        ErrorsModule.forRoot({
            required: 'Este campo es requerido',
            email: 'El email no es valido',
            passwordConfirmation: 'Las contraseñas no coinciden',
        }),
    ],
    exports: [],
    providers: [],
})
export class AuthModule {}
