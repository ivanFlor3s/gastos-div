import { Component, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NameValue } from '@app/models/view-models';

@Component({
    selector: 'app-select',
    templateUrl: './select.component.html',
    styleUrls: ['./select.component.scss'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: SelectComponent,
        },
    ],
})
export class SelectComponent implements ControlValueAccessor {
    @Input() options: NameValue<string>[] | null = [];

    constructor() {}

    onChange = (obj: any) => {};

    onTouched = () => {};

    touched = false;

    disabled = false;

    value: NameValue<string> | null;

    writeValue(val: NameValue<string>): void {
        this.value = this.options?.find((x) => x.value === val.value) || null;
    }
    registerOnChange(fn: any): void {
        this.onChange = fn;
    }
    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    ngOnInit() {}

    changeValue(val: any) {
        this.onChange(this.value);
    }
}
