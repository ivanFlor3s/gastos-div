declare var google: any;

import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { AppState, Login } from '@core/state';
import { take } from 'rxjs';
import { GoogleCredentials } from '@app/interfaces';

interface GoogleAuthResponse {
    clientId: string;
    client_id: string;
    credential: string;
    select_by: string;
}

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    private _fb = inject(FormBuilder);
    private _router = inject(Router);
    private _store = inject(Store);
    loginForm: FormGroup = this._fb.group({
        email: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(6)]],
    });

    public submit() {
        if (this.loginForm.invalid) {
            return;
        }

        this._store
            .dispatch(
                new Login(
                    this.loginForm.value.email,
                    this.loginForm.value.password
                )
            )
            .pipe(take(1))
            .subscribe({
                next: (_) => {
                    if (this._store.selectSnapshot(AppState.token)) {
                        this._router.navigate(['/dashboard']);
                    }
                },
            });
    }

    ngOnInit(): void {
        this.initGoogleSignIn();
    }

    private initGoogleSignIn() {
        google.accounts.id.initialize({
            client_id:
                '579234068570-8puotsfbni256pq4soousncsg6tdbkb9.apps.googleusercontent.com',
            callback: (response: GoogleAuthResponse) => {
                console.log(response);
                this.decodeToken(response.credential);
            },
        });

        google.accounts.id.renderButton(document.getElementById('google-btn'), {
            theme: 'filled_blue',
            size: 'large',
            shape: 'rectangular',
            width: '350',
            onsuccess: (response: any) => {
                console.log(response);
            },
            onfailure: (response: any) => {
                console.log(response);
            },
        });
    }

    private decodeToken(token: string): any {
        const decoded: GoogleCredentials = JSON.parse(
            atob(token.split('.')[1])
        );
        console.log(decoded);
    }
}
