import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserCreationDto } from '@app/models/dtos';
import { UsersService } from '@core/services';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit, OnDestroy {
    private _fb = inject(FormBuilder);
    private _userService = inject(UsersService);

    private confirmSub: Subscription;

    registerForm = this._fb.nonNullable.group({
        firstName: [
            '',
            [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(20),
            ],
        ],
        lastName: [
            '',
            [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(20),
            ],
        ],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        passwordConfirmation: [
            '',
            [Validators.required, Validators.minLength(6)],
        ],
    });

    ngOnInit(): void {
        this.initListenerToValidatePasswordConfirmation();
    }

    initListenerToValidatePasswordConfirmation() {
        const passConfirmControl =
            this.registerForm.controls.passwordConfirmation;

        this.confirmSub = passConfirmControl.valueChanges.subscribe((value) => {
            const passwordControl = this.registerForm.controls.password;

            if (passwordControl.value !== value) {
                passConfirmControl.setErrors({
                    passwordConfirmation: true,
                });
            } else {
                passConfirmControl.setErrors(null);
            }
        });
    }

    submit() {
        if (this.registerForm.valid) {
            const dto = this.registerForm.value as UserCreationDto;
            this._userService.createUser(dto).subscribe(console.log);
        }
    }

    ngOnDestroy(): void {
        this.confirmSub?.unsubscribe();
    }
}
