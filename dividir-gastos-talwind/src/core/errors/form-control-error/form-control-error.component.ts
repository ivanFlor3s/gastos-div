import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-form-control-error',
    templateUrl: './form-control-error.component.html',
    styleUrls: ['./form-control-error.component.css'],
})
export class FormControlErrorComponent {
    @Input() message = '';
    @Input() error: any;
}
