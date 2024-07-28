import { Component, Input, OnInit } from '@angular/core';
import { SpentItem } from '@app/models/dtos';
import { AppState, DeleteSpent } from '@core/state';
import { Store } from '@ngxs/store';
import Swal from 'sweetalert2';

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

    delete() {
        Swal.fire({
            title: '¿Estás seguro?',
            text: 'No podrás revertir esto',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, borrar',
        }).then((result) => {
            if (result.isConfirmed) {
                this.store.dispatch(new DeleteSpent(this.spent.id));
            }
        });
    }
}
