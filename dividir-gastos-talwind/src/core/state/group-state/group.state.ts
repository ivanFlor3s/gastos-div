import { Injectable } from '@angular/core';
import { BasicGroupVM, GroupVM } from '@app/models/view-models';
import { GroupsService, SpentsService } from '@core/services';
import { Action, Selector, State, StateContext, Store } from '@ngxs/store';
import {
    AddGroup,
    DeleteSpent,
    GetSpent,
    SetEditingGroup,
    SetEditingSpent,
    SetErrorInGroupDetail,
    StartAddSpent,
    StartCreatingGroups,
    StartEditingGroup,
    StartGettingBasicGroup,
    StartGettingGroup,
    StartGettingGroups,
    StartRemoveGroup,
} from './group.actions';
import { finalize, take, tap } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { GettingGroupErrorType } from '@app/interfaces/getting-group-error';
import { GroupDetail } from '@app/interfaces/group.detail';
import { ToastrService } from 'ngx-toastr';
import { SpentItem } from '@app/models/dtos';
import { GetCurrentUserId } from '@core/utils';

export interface GroupStateModel {
    groups: GroupVM[];
    detail: GroupDetail;
    error: GettingGroupError;
    editingSpent: SpentItem | null;
    editingGroup: BasicGroupVM | null;
}

export class GettingGroupError {
    constructor(
        public show: boolean,
        public message: string,
        public type: GettingGroupErrorType
    ) {}
}

const defaultState: GroupStateModel = {
    groups: [],
    detail: {
        group: null,
        spents: [],
    },
    editingSpent: null,
    editingGroup: null,
    error: {
        show: false,
        message: '',
        type: null,
    },
};

@State<GroupStateModel>({
    name: 'group',
    defaults: defaultState,
})
@Injectable()
export class GroupState {
    @Selector()
    static groups(state: GroupStateModel) {
        return state.groups;
    }

    @Selector()
    static error(state: GroupStateModel) {
        return state.error?.show ? state.error : null;
    }

    @Selector()
    static currentUserIsAdmin(state: GroupStateModel) {
        const currentId = GetCurrentUserId();
        const userInMembers = state.detail.group?.users.find(
            (u) => u.id == currentId
        );
        return userInMembers?.isAdmin;
    }

    @Selector()
    static detail(state: GroupStateModel) {
        return state.detail;
    }

    @Selector()
    static usersInDetail(state: GroupStateModel) {
        return state.detail.group?.users;
    }

    @Selector()
    static spentsInDetail(state: GroupStateModel) {
        return state.detail?.spents || [];
    }

    @Selector()
    static editingSpent(state: GroupStateModel) {
        return state.editingSpent;
    }

    @Selector()
    static editingGroup(state: GroupStateModel) {
        return state.editingGroup;
    }

    constructor(
        private _groupService: GroupsService,
        private _spentService: SpentsService,
        private _spinner: NgxSpinnerService,
        private _toastr: ToastrService
    ) {}

    @Action(StartGettingGroups)
    startGettingGroups(
        ctx: StateContext<GroupStateModel>,
        { name }: StartGettingGroups
    ) {
        this._spinner.show('groups');
        return this._groupService.getGroups(name).pipe(
            take(1),
            finalize(() => this._spinner.hide('groups')),
            tap({
                next(res) {
                    ctx.patchState({ groups: res });
                },
            })
        );
    }

    @Action(StartCreatingGroups)
    startCreatingGroups(
        ctx: StateContext<GroupStateModel>,
        { body }: StartCreatingGroups
    ) {
        this._spinner.show('newGroup');
        return this._groupService.createGroup(body).pipe(
            take(1),
            finalize(() => this._spinner.hide('newGroup')),
            tap({
                next(res: GroupVM) {
                    ctx.dispatch(new AddGroup(res));
                },
            })
        );
    }

    @Action(AddGroup)
    addGroup(ctx: StateContext<GroupStateModel>, { group }: AddGroup) {
        ctx.patchState({
            groups: [...ctx.getState().groups, group],
        });
    }

    @Action(StartGettingGroup)
    startGettingGroup(
        ctx: StateContext<GroupStateModel>,
        { groupId }: StartGettingGroup
    ) {
        return this._groupService.getGroup(groupId).pipe(
            tap({
                next(res) {
                    ctx.patchState({
                        detail: {
                            ...ctx.getState().detail,
                            group: res,
                            spents: res.spents || [],
                        },
                        error: { message: '', show: false, type: null },
                    });
                },
                error(err: HttpErrorResponse) {
                    if (err.status == HttpStatusCode.NotFound) {
                        ctx.dispatch(
                            new SetErrorInGroupDetail(
                                new GettingGroupError(
                                    true,
                                    'No se encontró el grupo',
                                    'NotFound'
                                )
                            )
                        );
                    } else if (err.status == HttpStatusCode.Unauthorized) {
                        ctx.dispatch(
                            new SetErrorInGroupDetail(
                                new GettingGroupError(
                                    true,
                                    'No tienes los permisos para ver este grupo',
                                    'Unauthorized'
                                )
                            )
                        );
                    } else {
                        ctx.dispatch(
                            new SetErrorInGroupDetail(
                                new GettingGroupError(
                                    true,
                                    'Ocurrió un error inesperado',
                                    'UnexpectedError'
                                )
                            )
                        );
                    }
                },
            })
        );
    }

    @Action(SetErrorInGroupDetail)
    setErrorInGroupDetail(
        ctx: StateContext<GroupStateModel>,
        { error }: SetErrorInGroupDetail
    ) {
        ctx.patchState({
            error,
        });
    }

    @Action(StartAddSpent)
    addSpent(ctx: StateContext<GroupStateModel>, { body }: StartAddSpent) {
        const group = ctx.getState().detail.group;

        if (group == null) {
            console.error('No hay grupo');
            return;
        }

        return this._spentService.addSpent(group.id, body).pipe(
            take(1),
            // finalize(() => this._spinner.hide('addSpent'))
            tap((_) => {
                const groupDetail = ctx.getState().detail.group;
                if (!groupDetail) return;
                this._toastr.success('Gasto agregado', '🎉');
                ctx.dispatch(new StartGettingGroup(groupDetail.id));
            })
        );
    }

    @Action(DeleteSpent)
    deleteSpent(ctx: StateContext<GroupStateModel>, { spentId }: DeleteSpent) {
        const group = ctx.getState().detail.group;
        if (group == null) {
            console.error('No hay grupo');
            return;
        }
        return this._spentService.delete(group.id, spentId).pipe(
            take(1),
            tap((_) => {
                const groupDetail = ctx.getState().detail.group;
                if (!groupDetail) return;
                this._toastr.success('Gasto eliminado', '🎉');
                ctx.dispatch(new StartGettingGroup(groupDetail.id));
            })
        );
    }

    @Action(GetSpent)
    getSpent(ctx: StateContext<GroupStateModel>, { spentId }: GetSpent) {
        const group = ctx.getState().detail.group;
        if (group == null) {
            console.error('No hay grupo');
            return;
        }
        return this._spentService.getSpent(group.id, spentId).pipe(
            take(1),
            tap((spent) => {
                ctx.dispatch(new SetEditingSpent(spent));
            })
        );
    }

    @Action(SetEditingSpent)
    setEditingSpent(
        ctx: StateContext<GroupStateModel>,
        { spent }: SetEditingSpent
    ) {
        ctx.patchState({ editingSpent: spent });
    }

    @Action(StartGettingBasicGroup)
    startGettingBasicGroup(
        ctx: StateContext<GroupStateModel>,
        { groupId }: StartGettingBasicGroup
    ) {
        return this._groupService.getBasicGroup(groupId, true).pipe(
            tap({
                next(res) {
                    ctx.dispatch(new SetEditingGroup(res));
                },
            })
        );
    }

    @Action(SetEditingGroup)
    setEditingGroup(
        ctx: StateContext<GroupStateModel>,
        { group }: SetEditingGroup
    ) {
        ctx.patchState({ editingGroup: group });
    }

    @Action(StartEditingGroup)
    startEditingGroup(
        ctx: StateContext<GroupStateModel>,
        { groupId, body }: StartEditingGroup
    ) {
        return this._groupService.editGroup(groupId, body).pipe(
            take(1),
            tap((_) => {
                this._toastr.success('Grupo editado', '🎉');
                ctx.dispatch(new StartGettingGroups(''));
            })
        );
    }

    @Action(StartRemoveGroup)
    startRemoveGroup(
        ctx: StateContext<GroupStateModel>,
        { groupId }: StartRemoveGroup
    ) {
        return this._groupService.removeGroup(groupId).pipe(
            take(1),
            tap((_) => {
                this._toastr.success('Grupo eliminado', '🎉');
                ctx.dispatch(new StartGettingGroups(''));
            })
        );
    }
}
