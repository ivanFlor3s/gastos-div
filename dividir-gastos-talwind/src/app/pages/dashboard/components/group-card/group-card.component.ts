import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GroupVM } from '@app/models/view-models';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NewGroupComponent } from '../modals/new-group/new-group.component';

@Component({
    selector: 'app-group-card',
    templateUrl: './group-card.component.html',
    styleUrls: ['./group-card.component.scss'],
})
export class GroupCardComponent {
    @Input() group: GroupVM;

    constructor(private _modalService: NgbModal) {}

    edit(event: MouseEvent) {
        event.stopPropagation();

        const modalRef = this._modalService.open(NewGroupComponent);
        modalRef.componentInstance.editing = true;
        modalRef.componentInstance.groupId = this.group.id;
    }
}
