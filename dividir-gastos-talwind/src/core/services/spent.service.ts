import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { AddSpentDto, SpentItem } from '@app/models/dtos';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class SpentsService {
    private _http = inject(HttpClient);

    addSpent(groupId: number, dto: AddSpentDto) {
        return this._http.post(
            `${environment.API_URL}/groups/${groupId}/spents`,
            dto
        );
    }

    delete(groupId: number, spentId: number) {
        return this._http.delete(
            `${environment.API_URL}/groups/${groupId}/spents/${spentId}`
        );
    }

    getSpent(groupId: number, spentId: number) {
        return this._http.get<SpentItem>(
            `${environment.API_URL}/groups/${groupId}/spents/${spentId}`
        );
    }

    edit(groupId: number, spentId: number, dto: AddSpentDto) {
        return this._http.put(
            `${environment.API_URL}/groups/${groupId}/spents/${spentId}`,
            dto
        );
    }
}
