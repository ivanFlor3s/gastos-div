import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { NameValue, UserVM } from '@app/models/view-models';
import { AppState, GroupState } from '@core/state';
import { Select, Selector, Store } from '@ngxs/store';
import { Observable, map } from 'rxjs';

@Component({
    selector: 'app-add-spent',
    templateUrl: './add-spent.component.html',
    styleUrls: ['./add-spent.component.css'],
})
export class AddSpentComponent implements OnInit {
    @Select(GroupState.usersInDetail) usersInGroup$: Observable<UserVM[]>;

    participants$: Observable<NameValue<string>[]>;

    spentForm = this.fb.group({
        description: [''],
        amount: [{} as Number],
        //date: [''],
        users: [{} as NameValue<string>[]],
        by: [''],
        how: [''],
    });

    constructor(private fb: FormBuilder, private store: Store) {
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
        this.spentForm.get('by')?.setValue(currentUserId);
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
}
