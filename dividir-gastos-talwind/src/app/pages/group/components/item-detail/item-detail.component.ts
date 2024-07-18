import { Component, Input, OnInit } from '@angular/core';
import { SpentItem } from '@app/models/dtos';
import { AppState } from '@core/state';
import { Store } from '@ngxs/store';

@Component({
    selector: 'app-item-detail',
    templateUrl: './item-detail.component.html',
    styleUrls: ['./item-detail.component.css'],
})
export class ItemDetailComponent implements OnInit {
    @Input() spent: SpentItem;
    iPaid: boolean = false;

    constructor(private store: Store) {}

    ngOnInit() {
        const userId = this.store.selectSnapshot(AppState.userId) as string;
        this.iPaid = this.spent.authorId === userId;
    }
}
