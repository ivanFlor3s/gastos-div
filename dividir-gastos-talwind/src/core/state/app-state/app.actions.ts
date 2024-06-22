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
