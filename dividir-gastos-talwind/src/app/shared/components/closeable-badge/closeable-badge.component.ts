import { Component, HostListener, OnInit, Output } from '@angular/core';
import {
    trigger,
    state,
    style,
    animate,
    transition,
} from '@angular/animations';

@Component({
    selector: 'app-closeable-badge',
    templateUrl: './closeable-badge.component.html',
    styleUrls: ['./closeable-badge.component.css'],
    animations: [
        trigger('widthHover', [
            transition(':enter', [
                style({ width: '0', overflow: 'hidden' }),
                animate('100ms', style({ width: '15px', overflow: 'unset' })),
            ]),
        ]),
    ],
})
export class CloseableBadgeComponent implements OnInit {
    hovered = false;

    @HostListener('mouseenter') onMouseEnter() {
        this.hovered = true;
    }
    @HostListener('mouseleave') onMouseLeave() {
        this.hovered = false;
    }

    constructor() {}

    ngOnInit() {}
}
