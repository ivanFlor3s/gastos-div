import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AddSpentDto } from '@app/models/dtos';
import { SpentMode } from '@app/models/enums/spent-mode.enum';
import { NameValue, UserVM } from '@app/models/view-models';
import { CustomDateParserFormatter } from '@core/adapters';
import { SpentsService } from '@core/services';
import { AppState, GroupState } from '@core/state';
import { Mapper } from '@core/utils';
import {
    NgbActiveModal,
    NgbCalendar,
    NgbDateParserFormatter,
} from '@ng-bootstrap/ng-bootstrap';
import { Select, Selector, Store } from '@ngxs/store';
import { ToastrService } from 'ngx-toastr';
import { Observable, map, take } from 'rxjs';

@Component({
    selector: 'app-add-spent',
    templateUrl: './add-spent.component.html',
    styleUrls: ['./add-spent.component.css'],
    providers: [
        {
            provide: NgbDateParserFormatter,
            useClass: CustomDateParserFormatter,
        },
    ],
})
export class AddSpentComponent implements OnInit {
    @Select(GroupState.usersInDetail) usersInGroup$: Observable<UserVM[]>;

    participants$: Observable<NameValue<string>[]>;

    private _ngCalendar = inject(NgbCalendar);

    spentForm = this.fb.nonNullable.group({
        description: ['', [Validators.required, Validators.maxLength(50)]],
        amount: [
            null as Number | null,
            [Validators.required, Validators.max(10000000)],
        ],
        payedAt: [this._ngCalendar.getToday(), Validators.required],
        users: [{} as NameValue<string>[]],
        author: [{} as NameValue<string>, Validators.required],
        how: [{ name: 'Todos', value: '' } as NameValue<string>],
    });

    constructor(
        private fb: FormBuilder,
        private store: Store,
        private _spentService: SpentsService,
        private _activeModal: NgbActiveModal,
        private _toastr: ToastrService
    ) {
        this.participants$ = this.usersInGroup$.pipe(
            map((users) => {
                return users.map((x) => ({
                    name: `${x.firstName} ${x.lastName}`,
                    value: x.id,
                }));
            })
        );
    }

    ngOnInit() {
        this.spentForm
            .get('users')
            ?.setValue([{ name: 'Todos', value: 'all' }]);

        this.setCurrentUserInBy();

        this.initUsersListener();
    }

    private setCurrentUserInBy() {
        const currentUserId = this.store.selectSnapshot(AppState.userId) || '';
        const userFullName =
            this.store.selectSnapshot(AppState.userFullName) || '';
        this.spentForm
            .get('author')
            ?.setValue({ name: userFullName, value: currentUserId });
    }

    initUsersListener() {
        this.spentForm.get('users')?.valueChanges.subscribe((value) => {
            if (value?.length == 0) {
                const usersInGroup =
                    this.store
                        .selectSnapshot(GroupState.usersInDetail)
                        ?.map((x) => ({
                            name: `${x.firstName} ${x.lastName}`,
                            value: x.id,
                        })) || [];

                this.spentForm.get('users')?.setValue(usersInGroup);
            }
        });
    }

    submit() {
        if (this.spentForm.invalid) {
            this._toastr.error('Formulario invalido', '🤔');
            return;
        }

        const groupId = this.store.selectSnapshot(GroupState.detail)?.group
            ?.id as number;
        const body: AddSpentDto = {
            amount: this.spentForm.get('amount')?.value as number,
            description: this.spentForm.get('description')?.value as string,
            users: this.spentForm.get('users')?.value as NameValue<string>[],
            authorId: this.spentForm.get('author')?.value.value as string,
            how: SpentMode.EQUALLY,
            payedAt: Mapper.mapDate(this.spentForm.get('payedAt')?.value),
            groupId,
        };

        this._spentService
            .addSpent(body)
            .pipe(take(1))
            .subscribe((x) => {
                this._toastr.success('Gasto agregado', '👍');
                this._activeModal.close();
            });
    }
}
