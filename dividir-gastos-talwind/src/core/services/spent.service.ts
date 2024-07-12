import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { AddSpentDto } from '@app/models/dtos';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class SpentsService {
    private _http = inject(HttpClient);

    addSpent(dto: AddSpentDto) {
        return this._http.post(`${environment.API_URL}/spents`, dto);
    }
}
