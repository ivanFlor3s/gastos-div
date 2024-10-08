import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginResponse } from '@app/interfaces';
import { GoogleUserCreationDto, UserCreationDto } from '@app/models/dtos';
import { environment } from 'src/environments/environment';

// const API_ENDPOINT = 'https://localhost:7089';

@Injectable({
    providedIn: 'root',
})
export class UsersService {
    constructor(private http: HttpClient) {}

    createUser(user: UserCreationDto) {
        return this.http.post(`${environment.API_URL}/auth/registrar`, user);
    }

    googleSignIn(user: GoogleUserCreationDto) {
        return this.http.post<LoginResponse>(
            `${environment.API_URL}/auth/signInWithGoogle`,
            user
        );
    }
}
