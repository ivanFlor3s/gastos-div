import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GroupVM } from '@app/models/view-models';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NewGroupComponent } from '../modals/new-group/new-group.component';
import Swal from 'sweetalert2';
import { StartRemoveGroup } from '@core/state';
import { Store } from '@ngxs/store';

@Component({
    selector: 'app-group-card',
    templateUrl: './group-card.component.html',
    styleUrls: ['./group-card.component.scss'],
})
export class GroupCardComponent {
    @Input() group: GroupVM;

    constructor(private _modalService: NgbModal, private _store: Store) {}

    edit(event: MouseEvent) {
        event.stopPropagation();

        const modalRef = this._modalService.open(NewGroupComponent);
        modalRef.componentInstance.editing = true;
        modalRef.componentInstance.groupId = this.group.id;
    }

    remove(event: MouseEvent) {
        event.stopPropagation();
        Swal.fire({
            title: '¿Estás seguro?',
            text: 'No podrás recuperar este grupo',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, borrar',
            cancelButtonText: 'Cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                this._store.dispatch(new StartRemoveGroup(this.group.id));
            }
        });
    }
}
