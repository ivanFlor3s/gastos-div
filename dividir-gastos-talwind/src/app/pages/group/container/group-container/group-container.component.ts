import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupDetail } from '@app/interfaces/group.detail';
import {
    AppState,
    GettingGroupError,
    GroupState,
    StartGettingGroup,
    StartLeftGroup,
} from '@core/state';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { AddSpentComponent, ManageGroupModalComponent } from '../../components';
import { SpentItem } from '@app/models/dtos';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-group-container',
    templateUrl: './group-container.component.html',
    styleUrls: ['./group-container.component.css'],
})
export class GroupContainerComponent implements OnInit {
    @Select(GroupState.error) error$: Observable<GettingGroupError>;
    @Select(GroupState.detail) groupDetail$: Observable<GroupDetail>;
    @Select(GroupState.spentsInDetail) spents$: Observable<SpentItem[]>;

    @Select(GroupState.currentUserIsAdmin)
    currentUserIsAdmin$: Observable<boolean>;
    idGroup: number;

    constructor(
        private store: Store,
        private activatedRoute: ActivatedRoute,
        private modalService: NgbModal,
        private _router: Router
    ) {
        const { idGroup } = this.activatedRoute.snapshot.params;
        this.idGroup = idGroup;
        this.store.dispatch(new StartGettingGroup(idGroup));
    }

    openSpentModal() {
        const modalRef = this.modalService.open(AddSpentComponent);
    }

    ngOnInit() {}

    leaveGroup() {
        Swal.fire({
            title: '¿Desea abandonar el grupo?',
            text: 'No podrás revertir esto',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, abandonar',
            cancelButtonText: 'Cancelar',
        }).then((result) => {
            if (result.isConfirmed) {
                this.store.dispatch(new StartLeftGroup(this.idGroup));
                this._router.navigate(['/dashboard']);
            }
        });
    }

    openManagePartitipantsModal() {
        const modalRef = this.modalService.open(ManageGroupModalComponent);
        const members = this.store
            .selectSnapshot(GroupState.usersInDetail)
            ?.filter((u) => u.id !== this.store.selectSnapshot(AppState.userId))
            .map((u) => ({
                name: u.isTemporal ? u.email : u.fullName,
                value: u.id.toString(),
            }));
        modalRef.componentInstance.idGroup = this.idGroup;
        modalRef.componentInstance.members = members;
    }
}
