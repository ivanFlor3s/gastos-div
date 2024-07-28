import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
    AvatarComponent,
    AvatarGroupComponent,
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
        AvatarGroupComponent,
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
        NgbTooltipModule,
        AvatarGroupComponent,
    ],
    providers: [],
})
export class SharedModule {}
