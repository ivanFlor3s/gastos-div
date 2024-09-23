import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupContainerComponent } from './container';
import { GroupRoutingModule } from './group.routing';
import { AddSpentComponent, ItemDetailComponent } from './components';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from '@app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorsModule } from '@core/errors/errors.module';
import {
    NgbDatepickerModule,
    NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { ManageGroupModalComponent } from './components/manage-group-modal/manage-group-modal.component';

@NgModule({
    imports: [
        CommonModule,
        GroupRoutingModule,
        NgbDropdownModule,
        NgxSpinnerModule,
        SharedModule,
        ReactiveFormsModule,
        FormsModule,
        NgbDatepickerModule,
        ErrorsModule.forRoot({
            required: 'Este campo es requerido',
            maxLength: 'El texto es demasiado largo',
            max: 'Supera el monto maximo permitido',
            pattern: 'El formato no es correcto',
            email: 'El email no es correcto',
        }),
    ],
    declarations: [
        GroupContainerComponent,
        ItemDetailComponent,
        AddSpentComponent,
        ManageGroupModalComponent,
    ],
})
export class GroupModule {}
