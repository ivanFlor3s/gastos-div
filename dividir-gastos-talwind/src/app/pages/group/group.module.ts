import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupContainerComponent } from './container';
import { GroupRoutingModule } from './group.routing';
import { AddSpentComponent, ItemDetailComponent } from './components';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
    imports: [CommonModule, GroupRoutingModule, NgxSpinnerModule, SharedModule],
    declarations: [
        GroupContainerComponent,
        ItemDetailComponent,
        AddSpentComponent,
    ],
})
export class GroupModule {}
