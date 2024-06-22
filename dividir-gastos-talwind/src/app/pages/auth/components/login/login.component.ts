import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@core/services';
import { Store } from '@ngxs/store';
import { AppState, Login } from '@core/state';
import { take } from 'rxjs';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    private _fb = inject(FormBuilder);
    private _authService = inject(AuthService);
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
}
