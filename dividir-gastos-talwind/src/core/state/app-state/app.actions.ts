import { GoogleCredentials } from '@app/interfaces';
import { AppUserState } from './app.state';

export class Login {
    static readonly type = '[App] Login';
    constructor(public email: string, public password: string) {}
}

export class Logout {
    static readonly type = '[App] Logout';
}

export class RefreshToken {
    static readonly type = '[App] RefreshToken';
}

export class StartGoogleAuthentication {
    static readonly type = '[App] Start GoogleAuthentication';
    constructor(public googleCreds: GoogleCredentials) {}
}

export class SetLoggedInUser {
    static readonly type = '[App] Set Logged In User';
    constructor(public token: string, public expiration: string) {}
}
