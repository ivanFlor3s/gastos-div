import { Component, inject } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { CreateGroupRequest } from '@app/interfaces';
import {
    AppState,
    GroupState,
    StartCreatingGroups,
    StartGettingBasicGroup,
} from '@core/state';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngxs/store';
import { take } from 'rxjs';

@Component({
    selector: 'app-new-group',
    templateUrl: './new-group.component.html',
    styleUrls: ['./new-group.component.css'],
})
export class NewGroupComponent {
    editing: boolean = false;
    groupId: number;

    private _fb = inject(FormBuilder);
    private store = inject(Store);
    private modalService = inject(NgbModal);
    private _activeModal = inject(NgbActiveModal);

    currentEmail$ = this.store.select(AppState.email);

    get emails() {
        return this.form.get('emails') as FormArray;
    }

    form = this._fb.group({
        name: ['', Validators.required],
        description: ['', [Validators.required, Validators.maxLength(50)]],
        emails: this._fb.array([]),
    });

    ngOnInit(): void {
        if (this.editing) {
            this.store
                .dispatch(new StartGettingBasicGroup(this.groupId))
                .pipe(take(1))
                .subscribe((_) => {
                    const basicGroup = this.store.selectSnapshot(
                        GroupState.editingGroup
                    );
                    if (basicGroup) {
                        this.form.patchValue({
                            name: basicGroup.name,
                            description: basicGroup.description,
                        });
                        basicGroup.users.forEach((user) => {
                            this.emails.push(
                                this._fb.control(user.email, [Validators.email])
                            );
                        });
                    }
                });
        } else {
            this.addEmailInput();
        }
    }

    addEmailInput() {
        this.emails.push(this._fb.control('', [Validators.email]));
    }

    removeEmail(index: number) {
        this.emails.removeAt(index);
    }

    onSubmit() {
        if (this.form.invalid) {
            return;
        }

        this.store
            .dispatch(
                new StartCreatingGroups(this.form.value as CreateGroupRequest)
            )
            .subscribe(() => {
                this._activeModal.close();
            });
    }

    dismiss() {
        this.modalService.dismissAll();
    }
}
