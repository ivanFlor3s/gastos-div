import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { LoginResponse } from '@app/interfaces';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private _http = inject(HttpClient);

    login(email: string, password: string): Observable<LoginResponse> {
        return this._http.post<LoginResponse>(
            `${environment.API_URL}/auth/login`,
            {
                email,
                password,
            }
        );
    }
}
