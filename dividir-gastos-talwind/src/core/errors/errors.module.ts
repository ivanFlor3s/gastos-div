import { InjectionToken, ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MsgeError } from '@app/interfaces';
import { ValidationErrorMsgeService } from './services/error-validation-msge.service';
import { FormSubmitDirective } from './form-submit.directive';
import { ValidationMsgeDirective } from './validation-msge.directive';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControlErrorComponent } from './form-control-error/form-control-error.component';

export const MSGE_ERROR_TOKEN = new InjectionToken<MsgeError>('MsgeError');

@NgModule({
    declarations: [
        FormSubmitDirective,
        ValidationMsgeDirective,
        FormControlErrorComponent,
    ],
    imports: [CommonModule, ReactiveFormsModule, FormsModule],
    providers: [],
    exports: [
        FormSubmitDirective,
        ValidationMsgeDirective,
        FormControlErrorComponent,
    ],
})
export class ErrorsModule {
    static forRoot(config: MsgeError): ModuleWithProviders<ErrorsModule> {
        return {
            ngModule: ErrorsModule,
            providers: [
                ValidationErrorMsgeService,
                { provide: MSGE_ERROR_TOKEN, useValue: config },
            ],
        };
    }
}
