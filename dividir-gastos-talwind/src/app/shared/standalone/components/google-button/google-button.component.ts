import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-google-button',
    templateUrl: './google-button.component.html',
    styleUrls: ['./google-button.component.scss'],
    standalone: true,
})
export class GoogleButtonComponent implements OnInit {
    @Input() disabled = false;
    constructor() {}

    ngOnInit() {}
}
