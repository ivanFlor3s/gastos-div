import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateGroupRequest } from '@app/interfaces';
import { GroupsService } from '@core/services';
import { AppState, StartCreatingGroups } from '@core/state';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngxs/store';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize, take } from 'rxjs';

@Component({
    selector: 'app-new-group',
    templateUrl: './new-group.component.html',
    styleUrls: ['./new-group.component.css'],
})
export class NewGroupComponent {
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
        description: [''],
        emails: this._fb.array([]),
    });

    ngOnInit(): void {
        this.addEmailInput();
    }

    addEmailInput() {
        this.emails.push(
            this._fb.control('', [Validators.required, Validators.email])
        );
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
