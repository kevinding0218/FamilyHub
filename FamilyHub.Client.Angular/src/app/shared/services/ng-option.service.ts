import { IOptionResponse } from './response extension/api-response.config';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { IOption } from 'ng-select';
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

    public static readonly LANGUAGES: Array<IOption> = [
        { value: 'us', label: 'English (US)' },
        { value: 'cn', label: '中文' }
    ];

    getLanguages(): Observable<Array<IOption>> {
        return this.loadOptions(NgIOptionService.LANGUAGES);
    }

    private loadOptions(options: Array<IOption>): Observable<Array<IOption>> {
        return new Observable((obs) => {
            setTimeout(() => {
                obs.next(this.cloneOptions(options));
                obs.complete();
            }, 500);
        });
    }

    private cloneOptions(options: Array<IOption>): Array<IOption> {
        return options.map(option => ({ value: option.value, label: option.label }));
    }

    loadIOptionMembersRelationship(): Observable<IOptionResponse> {
        // console.log(`NgIOptionService Get: ${environment.apiUrl}/api/member/IOptionMemberRelationship`);
        return this.httpClient.get<IOptionResponse>(`${environment.REMOTE_API_URL}/api/member/IOptionMemberRelationship`, httpOptions);
    }
}
