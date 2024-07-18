import { Injectable } from '@angular/core';
import { AuthService } from '@core/services';
import { Action, State, StateContext, Selector } from '@ngxs/store';
import { Login, Logout } from './app.actions';
import { catchError, tap } from 'rxjs';
import Swal from 'sweetalert2';
import jwt_decode from 'jwt-decode';
import { TokenPayload } from '@app/interfaces';
import { UserVM } from '@app/models/view-models';

const CLAIM_BASE = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims';

export interface AppUserState extends UserVM {
    fullName: string;
}

export interface AppStateModel {
    token: string;
    expiration: string;
    user: AppUserState | null;
}

const defaultAppState: AppStateModel = {
    token: '',
    expiration: '',
    user: null,
};

@State<AppStateModel>({
    name: 'app',
    defaults: defaultAppState,
})
@Injectable()
export class AppState {
    @Selector()
    static token(state: AppStateModel) {
        return state.token;
    }

    @Selector()
    static email(state: AppStateModel) {
        return state.user?.email;
    }

    @Selector()
    static userId(state: AppStateModel) {
        return state.user?.id;
    }

    @Selector()
    static userFullName(state: AppStateModel) {
        return state.user?.fullName;
    }

    constructor(private _authService: AuthService) {}

    @Action(Login)
    login(ctx: StateContext<AppStateModel>, { email, password }: Login) {
        return this._authService.login(email, password).pipe(
            catchError((err) => {
                ctx.patchState(defaultAppState);
                Swal.fire({
                    icon: 'error',
                    title: 'Ocurrio un error',
                });
                throw err.error;
            }),
            tap({
                next(res) {
                    const { token, expiration } = res;
                    const tokenDecoded = jwt_decode(token) as any;
                    console.log(tokenDecoded);

                    ctx.patchState({
                        token,
                        expiration,
                        user: {
                            id: tokenDecoded[`${CLAIM_BASE}/nameidentifier`],
                            email: tokenDecoded[`${CLAIM_BASE}/emailaddress`],
                            firstName: tokenDecoded[`${CLAIM_BASE}/name`],
                            lastName: tokenDecoded[`${CLAIM_BASE}/surname`],
                            fullName: `${tokenDecoded[`${CLAIM_BASE}/name`]} ${
                                tokenDecoded[`${CLAIM_BASE}/surname`]
                            }`,
                        },
                    });
                },
                error(err) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error al iniciar sesión',
                        text: err.error.error,
                    });
                },
            })
        );
    }

    @Action(Logout)
    logout({ setState }: StateContext<AppStateModel>) {
        setState(defaultAppState);
    }
}
