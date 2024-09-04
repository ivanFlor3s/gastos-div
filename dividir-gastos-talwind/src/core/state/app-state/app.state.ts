import { Injectable } from '@angular/core';
import { AuthService, UsersService } from '@core/services';
import { Action, State, StateContext, Selector } from '@ngxs/store';
import {
    Login,
    Logout,
    SetLoggedInUser,
    StartGoogleAuthentication,
} from './app.actions';
import { catchError, tap } from 'rxjs';
import Swal from 'sweetalert2';
import jwt_decode from 'jwt-decode';
import { LoginResponse, TokenPayload } from '@app/interfaces';
import { UserVM } from '@app/models/view-models';
import { UserCreationDto } from '@app/models/dtos';
import { GoogleUserCreationDto } from '../../../app/models/dtos/google-user-creation.dto';
import { Helper } from '@core/utils';
import { Router } from '@angular/router';

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

    constructor(
        private _authService: AuthService,
        private _userService: UsersService,
        private _router: Router
    ) {}

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
                    ctx.dispatch(
                        new SetLoggedInUser(res.token, res.expiration)
                    );
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

    @Action(SetLoggedInUser)
    setLoggedInUser(
        ctx: StateContext<AppStateModel>,
        { token, expiration }: SetLoggedInUser
    ) {
        const tokenDecoded = jwt_decode(token) as any;

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
        this._router.navigate(['/dashboard']);
    }

    @Action(Logout)
    logout({ setState }: StateContext<AppStateModel>) {
        setState(defaultAppState);
    }

    @Action(StartGoogleAuthentication)
    startGoogleSign(
        ctx: StateContext<AppStateModel>,
        { googleCreds }: StartGoogleAuthentication
    ) {
        const user: GoogleUserCreationDto = {
            email: googleCreds.email,
            firstName: googleCreds.given_name,
            lastName: googleCreds.family_name,
            imageUrl: googleCreds.picture,
            password: Helper.generateRandomPassword(12),
        };

        return this._userService.googleSignIn(user).pipe(
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
                    ctx.dispatch(
                        new SetLoggedInUser(res.token, res.expiration)
                    );
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
}
