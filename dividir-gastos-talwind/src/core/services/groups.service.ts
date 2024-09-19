import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Params } from '@angular/router';
import { CreateGroupRequest } from '@app/interfaces';
import { BasicGroupVM, GroupVM } from '@app/models/view-models';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class GroupsService {
    private _http = inject(HttpClient);

    getGroups(name: string): Observable<GroupVM[]> {
        return this._http.get<GroupVM[]>(`${environment.API_URL}/group`, {
            params: { name },
        });
    }

    createGroup(body: CreateGroupRequest): Observable<GroupVM> {
        return this._http.post<GroupVM>(`${environment.API_URL}/group`, body);
    }

    getGroup(groupId: number): Observable<GroupVM> {
        return this._http.get<GroupVM>(
            `${environment.API_URL}/group/${groupId}`
        );
    }

    getBasicGroup(
        groupId: number,
        excludeMySelf: boolean
    ): Observable<BasicGroupVM> {
        const params: Params = { excludeCurrentUser: excludeMySelf.toString() };
        return this._http.get<BasicGroupVM>(
            `${environment.API_URL}/group/${groupId}/basic`,
            { params }
        );
    }

    editGroup(groupId: number, body: CreateGroupRequest) {
        return this._http.put(`${environment.API_URL}/group/${groupId}`, body);
    }

    retrieve() {
        return this._http.put<void>(
            `${environment.API_URL}/group/retrieve`,
            {}
        );
    }
}
