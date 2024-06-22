import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from '@core/state';
import { Select, Store } from '@ngxs/store';
import { Observable, Subscription } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
    title = 'dividir-gastos';

    get inAuthUrl(): boolean {
        return this.router.url.includes('auth');
    }

    @Select(AppState.token) token$: Observable<string>;

    tokenSubscription: Subscription;

    constructor(private router: Router, private _store: Store) {}

    ngOnInit(): void {
        this.initTokenSub();
    }

    private initTokenSub() {
        this.tokenSubscription = this.token$.subscribe((token) => {
            if (!token) {
                this.router.navigateByUrl('/auth/login');
            }
        });
    }

    ngOnDestroy(): void {
        this.tokenSubscription?.unsubscribe();
    }
}
