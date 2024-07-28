import { Component, Input, OnInit, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AddSpentDto, SpentItem } from '@app/models/dtos';
import { SpentMode } from '@app/models/enums/spent-mode.enum';
import { NameValue, UserVM } from '@app/models/view-models';
import { CustomDateParserFormatter } from '@core/adapters';
import { SpentsService } from '@core/services';
import {
    AppState,
    GetSpent,
    GroupState,
    SetEditingSpent,
    StartAddSpent,
    StartGettingGroup,
} from '@core/state';
import { Mapper } from '@core/utils';
import {
    NgbActiveModal,
    NgbCalendar,
    NgbDateParserFormatter,
} from '@ng-bootstrap/ng-bootstrap';
import { Select, Store } from '@ngxs/store';
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
    @Input() spent: SpentItem;

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
    isEdit: boolean = false;

    constructor(
        private fb: FormBuilder,
        private store: Store,
        private _spentService: SpentsService,
        public activeModal: NgbActiveModal,
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

        if (this.spent) {
            this.isEdit = true;
            this.loadDataFromExistingSpent();
        }
    }
    loadDataFromExistingSpent() {
        this.store
            .dispatch(new GetSpent(this.spent.id))
            .pipe(take(1))
            .subscribe((_) => {
                const spentFromBack = this.store.selectSnapshot(
                    GroupState.editingSpent
                );
                if (!spentFromBack) return;
                const users = Mapper.mapUserVMsToNameValue(
                    spentFromBack.participants
                );
                this.spentForm.patchValue({
                    description: spentFromBack.description,
                    amount: spentFromBack.amount,
                    payedAt: Mapper.mapNgbDate(spentFromBack.payedAt),
                    users,
                    author: {
                        name: `${spentFromBack.author.firstName} ${spentFromBack.author.lastName}`,
                        value: spentFromBack.authorId,
                    },
                });
            });
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

        const body: AddSpentDto = this.buildDto();

        if (!this.isEdit) {
            this.store
                .dispatch(new StartAddSpent(body))
                .pipe(take(1))
                .subscribe(() => {
                    this.activeModal.close();
                });
        } else {
            const groupId = this.store.selectSnapshot(GroupState.detail)?.group
                ?.id;
            if (!groupId) return;
            this._spentService
                .edit(groupId, this.spent.id, body)
                .pipe(take(1))
                .subscribe(() => {
                    this.activeModal.close();
                    this.store.dispatch(new StartGettingGroup(groupId));
                    this._toastr.success('Gasto editado', '🎉');
                });
        }
    }

    private buildDto(): AddSpentDto {
        const dto: AddSpentDto = {
            amount: this.spentForm.get('amount')?.value as number,
            description: this.spentForm.get('description')?.value as string,
            participants: [],
            authorId: this.spentForm.get('author')?.value.value as string,
            how: SpentMode.EQUALLY,
            payedAt: Mapper.mapDate(this.spentForm.get('payedAt')?.value),
        };
        if (this.spentForm.get('users')?.value[0].value === 'all') {
            const participants =
                this.store.selectSnapshot(GroupState.usersInDetail) || [];
            dto.participants = participants.map((x) => ({
                name: `${x.firstName} ${x.lastName}`,
                value: x.id,
            }));
        } else {
            dto.participants = this.spentForm.get('users')?.value || [];
        }
        return dto;
    }

    ngOnDestroy(): void {
        //Called once, before the instance is destroyed.
        //Add 'implements OnDestroy' to the class.
        this.store.dispatch(new SetEditingSpent(null));
    }
}
