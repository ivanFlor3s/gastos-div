import { Component, Input, OnInit } from '@angular/core';
import { SpentItem } from '@app/models/dtos';

@Component({
    selector: 'app-item-detail',
    templateUrl: './item-detail.component.html',
    styleUrls: ['./item-detail.component.css'],
})
export class ItemDetailComponent implements OnInit {
    @Input() spent: SpentItem;

    constructor() {}

    ngOnInit() {}
}
