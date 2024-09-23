import { Component, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NameValue } from '@app/models/view-models';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-manage-group-modal',
    templateUrl: './manage-group-modal.component.html',
    styleUrls: ['./manage-group-modal.component.scss'],
})
export class ManageGroupModalComponent {
    @Input() members: NameValue<string>[];
    @Input() groupId: number;

    membersForm = this.fb.group({
        users: [{} as NameValue<string>[]],
        searchUser: ['', [Validators.email]],
    });

    constructor(public activeModal: NgbActiveModal, private fb: FormBuilder) {}

    ngOnInit(): void {
        this.membersForm.patchValue({ users: this.members });
    }

    addUser() {
        if (this.membersForm.invalid) return;

        const user = this.membersForm.get('searchUser')?.value;
        if (user) {
            this.membersForm.patchValue({
                users: [
                    ...(this.membersForm.get('users')?.value || []),
                    { name: user, value: user },
                ],
                searchUser: '',
            });
        }
    }
}
