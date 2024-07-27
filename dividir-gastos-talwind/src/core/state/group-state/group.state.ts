import { Injectable } from '@angular/core';
import { GroupVM } from '@app/models/view-models';
import { GroupsService, SpentsService } from '@core/services';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
    AddGroup,
    SetErrorInGroupDetail,
    StartAddSpent,
    StartCreatingGroups,
    StartGettingGroup,
    StartGettingGroups,
} from './group.actions';
import { finalize, take, tap } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { GettingGroupErrorType } from '@app/interfaces/getting-group-error';
import { GroupDetail } from '@app/interfaces/group.detail';
import { ToastrService } from 'ngx-toastr';
import { SpentItem } from '@app/models/dtos';

export interface GroupStateModel {
    groups: GroupVM[];
    detail: GroupDetail;
    error: GettingGroupError;
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
                    console.log('tremendo error', err);
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
        return this._spentService.addSpent(body).pipe(
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
}
