import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { UserVM } from '@app/models/view-models';

@Component({
    selector: 'app-avatar',
    templateUrl: './avatar.component.html',
    styleUrls: ['./avatar.component.css'],
})
export class AvatarComponent implements OnInit {
    @Input() user: UserVM;

    avatarName: string;

    constructor() {}

    ngOnInit() {}

    ngOnChanges(changes: SimpleChanges): void {
        this.avatarName =
            this.user?.firstName?.charAt(0) + this.user?.lastName?.charAt(0);
    }
}
