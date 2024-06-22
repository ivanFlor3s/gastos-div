import { Component } from '@angular/core';

@Component({
    selector: 'app-dropdown',
    templateUrl: './dropdown.component.html',
    styleUrls: ['./dropdown.component.css'],
})
export class DropdownComponent {
    showContent = false;

    toggleContent() {
        this.showContent = !this.showContent;
    }
}
