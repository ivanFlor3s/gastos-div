import { Component, Input, OnInit } from '@angular/core';
import { GroupMemberVM } from '@app/models/view-models';

@Component({
    selector: 'app-avatar-group',
    templateUrl: './avatar-group.component.html',
    styleUrls: ['./avatar-group.component.scss'],
})
export class AvatarGroupComponent implements OnInit {
    @Input() users: GroupMemberVM[] = [];
    constructor() {}

    ngOnInit() {}
}
