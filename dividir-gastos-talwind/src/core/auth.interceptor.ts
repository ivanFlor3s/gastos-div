/* eslint-disable @typescript-eslint/no-explicit-any */
import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap, throwError } from 'rxjs';
import { Store } from '@ngxs/store';
import { AppState, RefreshToken } from './state';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private _store: Store, private _router: Router) {}

    intercept(
        req: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        const refreshTokenEndpoint = '/auth/refresh-token';
        const token = this._store.selectSnapshot(AppState.token);

        if (token) {
            const cloned = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${token}`),
            });
            return next.handle(cloned).pipe(
                catchError((x) => this.handleAuthError(x)),
                tap({
                    next: (_) => {
                        if (!req.url.includes(refreshTokenEndpoint)) {
                            this._store.dispatch(new RefreshToken());
                        }
                    },
                })
            );
        } else {
            return next
                .handle(req)
                .pipe(catchError((x) => this.handleAuthError(x)));
        }
    }

    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        //handle your auth error or rethrow
        if (err.status == 0 || err.status === 401 || err.status === 403) {
            //navigate /delete cookies or whatever
            this._router.navigateByUrl(`/auth/login`);
            // if you've caught / handled the error, you don't want to rethrow it unless you also want downstream consumers to have to handle it as well.
            return of(err); // or EMPTY may be appropriate here
        }
        return throwError(() => err);
    }
}
