import { Injectable } from '@angular/core';
import { AuthService } from '@core/services';
import { Action, State, StateContext, Selector } from '@ngxs/store';
import { Login, Logout } from './app.actions';
import { catchError, tap } from 'rxjs';
import Swal from 'sweetalert2';
import jwt_decode from 'jwt-decode';
import { TokenPayload } from '@app/interfaces';

export interface AppStateModel {
    token: string;
    expiration: string;
    email: string;
}

const defaultAppState: AppStateModel = {
    token: '',
    expiration: '',
    email: '',
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
        return state.email;
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
                    const { email } = jwt_decode(token) as TokenPayload;
                    ctx.patchState({ token, expiration, email });
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
