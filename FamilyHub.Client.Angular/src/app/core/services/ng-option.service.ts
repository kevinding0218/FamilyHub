import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { IOptionResponse } from '../config/api-response.config';
import { Observable } from 'rxjs/Observable';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
    })
};

@Injectable()
export class NgIOptionService {
    constructor(private httpClient: HttpClient) { }

    loadIOptionMembersRelationship(): Observable<IOptionResponse> {
        console.log(`Get Request From: ${environment.apiUrl}/api/member/IOptionMemberRelationship`);
        return this.httpClient.get<IOptionResponse>(`${environment.apiUrl}/api/member/IOptionMemberRelationship`, httpOptions);
    }
}
