import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupContainerComponent } from './container';
import { GroupRoutingModule } from './group.routing';
import { AddSpentComponent, ItemDetailComponent } from './components';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from '@app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorsModule } from '@core/errors/errors.module';

@NgModule({
    imports: [
        CommonModule,
        GroupRoutingModule,
        NgxSpinnerModule,
        SharedModule,
        ReactiveFormsModule,
        FormsModule,
        ErrorsModule.forRoot({
            required: 'Este campo es requerido',
            maxLength: 'El texto es demasiado largo',
            max: 'Supera el monto maximo permitido',
        }),
    ],
    declarations: [
        GroupContainerComponent,
        ItemDetailComponent,
        AddSpentComponent,
    ],
})
export class GroupModule {}
