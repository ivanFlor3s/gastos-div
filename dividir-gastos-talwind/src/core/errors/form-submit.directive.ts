import {
    Directive,
    ElementRef,
    Injector,
    OnDestroy,
    OnInit,
    Self,
} from '@angular/core';
import { FormGroup, FormGroupDirective } from '@angular/forms';
import { fromEvent, shareReplay, Subscription } from 'rxjs';

const markAllControls = (form: FormGroup) => {
    Object.keys(form.controls).forEach((k) => {
        const control = form.get(k);
        control?.markAsDirty();
        control?.updateValueAndValidity();
    });
};

@Directive({
    // eslint-disable-next-line @angular-eslint/directive-selector
    selector: '[formGroup]',
})
export class FormSubmitDirective implements OnInit, OnDestroy {
    submitSub!: Subscription;
    submit$ = fromEvent(this.element, 'submit').pipe(shareReplay(1));

    get element() {
        return this.host.nativeElement;
    }
    constructor(
        private injector: Injector,
        private host: ElementRef<HTMLFormElement>,
        @Self() private fd: FormGroupDirective
    ) {}

    ngOnInit(): void {
        this.submitSub = this.submit$.subscribe((_) => {
            markAllControls(this.fd.form);
        });
    }

    ngOnDestroy(): void {
        this.submitSub?.unsubscribe();
    }
}
