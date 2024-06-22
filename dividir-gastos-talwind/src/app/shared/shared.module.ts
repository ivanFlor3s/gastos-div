import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
    AvatarComponent,
    CloseableBadgeComponent,
    DropdownComponent,
    NavbarComponent,
    SearchComponent,
    UsersListComponent,
} from './components';
import { ClickOutsideDirective } from './directives';

import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    declarations: [
        DropdownComponent,
        NavbarComponent,
        ClickOutsideDirective,
        SearchComponent,
        UsersListComponent,
        CloseableBadgeComponent,
        AvatarComponent,
    ],
    imports: [CommonModule, NgbTooltipModule],
    exports: [
        DropdownComponent,
        NavbarComponent,
        ClickOutsideDirective,
        SearchComponent,
        UsersListComponent,
        CloseableBadgeComponent,
        AvatarComponent,
    ],
    providers: [],
})
export class SharedModule {}
