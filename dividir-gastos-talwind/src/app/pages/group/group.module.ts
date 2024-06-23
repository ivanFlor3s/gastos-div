import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupContainerComponent } from './container';
import { GroupRoutingModule } from './group.routing';
import { AddSpentComponent, ItemDetailComponent } from './components';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from '@app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        CommonModule,
        GroupRoutingModule,
        NgxSpinnerModule,
        SharedModule,
        ReactiveFormsModule,
        FormsModule,
    ],
    declarations: [
        GroupContainerComponent,
        ItemDetailComponent,
        AddSpentComponent,
    ],
})
export class GroupModule {}
