import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
    AvatarComponent,
    CloseableBadgeComponent,
    DropdownComponent,
    NavbarComponent,
    SearchComponent,
    SelectComponent,
    UsersListComponent,
} from './components';
import { ClickOutsideDirective } from './directives';

import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        DropdownComponent,
        NavbarComponent,
        ClickOutsideDirective,
        SearchComponent,
        UsersListComponent,
        CloseableBadgeComponent,
        AvatarComponent,
        SelectComponent,
    ],
    imports: [CommonModule, NgbTooltipModule, ReactiveFormsModule, FormsModule],
    exports: [
        DropdownComponent,
        NavbarComponent,
        ClickOutsideDirective,
        SearchComponent,
        UsersListComponent,
        CloseableBadgeComponent,
        AvatarComponent,
        SelectComponent,
    ],
    providers: [],
})
export class SharedModule {}
