import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GroupDetail } from '@app/interfaces/group.detail';
import { GettingGroupError, GroupState, StartGettingGroup } from '@core/state';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { AddSpentComponent } from '../../components';
import { SpentItem } from '@app/models/dtos';

@Component({
    selector: 'app-group-container',
    templateUrl: './group-container.component.html',
    styleUrls: ['./group-container.component.css'],
})
export class GroupContainerComponent implements OnInit {
    @Select(GroupState.error) error$: Observable<GettingGroupError>;
    @Select(GroupState.detail) groupDetail$: Observable<GroupDetail>;
    @Select(GroupState.spentsInDetail) spents$: Observable<SpentItem[]>;

    constructor(
        private store: Store,
        private activatedRoute: ActivatedRoute,
        private modalService: NgbModal
    ) {
        const { idGroup } = this.activatedRoute.snapshot.params;
        console.log(idGroup);

        this.store.dispatch(new StartGettingGroup(idGroup));
    }

    openSpentModal() {
        const modalRef = this.modalService.open(AddSpentComponent);
    }

    ngOnInit() {}
}
