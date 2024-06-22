import { Component, Input } from '@angular/core';
import { GroupVM } from '@app/models/view-models';

@Component({
    selector: 'app-group-card',
    templateUrl: './group-card.component.html',
    styleUrls: ['./group-card.component.scss'],
})
export class GroupCardComponent {
    @Input() group: GroupVM;
}
