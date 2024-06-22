import { Component, inject } from '@angular/core';
import { AppState, Logout } from '@core/state';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
    @Select(AppState.email) email$: Observable<string>;

    showMenu = false;
    _store = inject(Store);

    toggleMenu() {
        this.showMenu = !this.showMenu;
    }

    logout() {
        this._store.dispatch(new Logout());
    }
}
