import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { GroupMemberVM } from '@app/models/view-models';

@Component({
    selector: 'app-avatar',
    templateUrl: './avatar.component.html',
    styleUrls: ['./avatar.component.css'],
})
export class AvatarComponent implements OnInit {
    @Input() user: GroupMemberVM;
    @Input() borderClass: string;

    @Input() showAdminIcon: boolean;
    @Input() showTemporalIcon: boolean;

    avatarName: string;

    constructor() {}

    ngOnInit() {}

    ngOnChanges(changes: SimpleChanges): void {
        this.setAvatarName();
    }

    private setAvatarName() {
        const avatar2letters =
            this.user.firstName.charAt(0) + this.user.lastName.charAt(0);
        this.avatarName = !this.user.isTemporal
            ? avatar2letters.toUpperCase()
            : this.user.email.charAt(0).toUpperCase();
    }
}
