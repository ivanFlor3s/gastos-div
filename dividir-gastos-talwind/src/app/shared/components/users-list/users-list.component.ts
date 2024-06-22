import { Component } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NameValue } from '@app/models/view-models';

@Component({
    selector: 'app-users-list',
    templateUrl: './users-list.component.html',
    styleUrls: ['./users-list.component.scss'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: UsersListComponent,
        },
    ],
})
export class UsersListComponent implements ControlValueAccessor {
    onChange = (users: NameValue<string>[]) => {};

    onTouched = () => {};

    touched = false;

    disabled = false;

    values: NameValue<string>[] = [];

    writeValue(obj: []): void {
        this.values = obj;
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

    remove(item: NameValue<string>) {
        this.values = this.values.filter((value) => value !== item);
        this.onChange(this.values);
    }
}
