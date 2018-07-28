import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import 'rxjs/add/operator/map';

import * as moment from 'moment';
import { environment } from './../../../environments/environment';
import { ActionState } from '../../shared/services/action.config';
import { IResponse, ISingleResponse, IListResponse, ResponseMessage } from './response extension/api-response.config';

export interface MemberDetailRequest {
    memberContactID: number;
    firstName: string;
    lastName: string;
    mobilePhone: string;
    homePhone: string;
    location: string;
    emailAddress: string;
    memberRelationshipID: string;
}

export interface MemberContactListResponse extends MemberDetailRequest {
    fullName: string;
    contactPhone: string;
    createdOn: string;
    memberRelationshipName: string;
    imageSource: string;
}

// tslint:disable-next-line:no-empty-interface
export interface MemberDetailResponse extends MemberContactListResponse {

}

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json'
    })
};

@Injectable()
export class MemberService {
    private url: string;
    private endpoint: string;

    // #region Subject
    memberDetailAction$ = new Subject<{ action: string, dataModel: MemberDetailResponse, dataIndex?: number }>();
    // #endregion

    constructor(private httpClient: HttpClient) {
        this.url = `${environment.REMOTE_API_URL}`;
        this.endpoint = 'api/member';
    }

    listMemberContact(uid: number): Observable<IListResponse> {
        return this.httpClient.get<IListResponse>(`${this.url}/${this.endpoint}/memberContactByCreated/${uid}`)
            .map((listResponse) => {
                if (listResponse.message === ResponseMessage.Success) {
                    listResponse.model.forEach(element => {
                        this.formatModel(element);
                    });
                }
                return listResponse;
            });
    }

    createMemberDetail(newMemberDetail: MemberDetailRequest): Observable<ISingleResponse> {
        console.log('createMemberDetail newMemberDetail:', newMemberDetail);
        return this.httpClient.post<ISingleResponse>(`${this.url}/${this.endpoint}/createMemberContact`, newMemberDetail, httpOptions);
    }

    afterCreateMemberDetail(memberDetail: MemberDetailResponse) {
        this.formatModel(memberDetail);
        this.memberDetailAction$.next({ action: ActionState.CREATE, dataModel: memberDetail });
    }

    updateMemberDetail(memberContactId: number, updateMemberDetail: MemberDetailRequest): Observable<ISingleResponse> {
        console.log('updateMemberDetail:', updateMemberDetail);
        return this.httpClient.put<ISingleResponse>(`${this.url}/${this.endpoint}/updateMemberContact/${memberContactId}`,
            updateMemberDetail, httpOptions);
    }

    afterUpdateMemberDetail(memberDetail: MemberDetailResponse, memberDetailIndex: number) {
        this.formatModel(memberDetail);
        this.memberDetailAction$.next({ action: ActionState.UPDATE, dataModel: memberDetail, dataIndex: memberDetailIndex });
    }

    deleteMemberDetail(memberContactId: number) {
        console.log('deleteMemberDetail:', memberContactId);
        return this.httpClient.delete<IResponse>(`${this.url}/${this.endpoint}/deleteMemberContact/${memberContactId}`, httpOptions);
    }

    afterDeleteMemberDetail(memberDetail: MemberDetailResponse, memberDetailIndex: number) {
        this.memberDetailAction$.next({ action: ActionState.DELETE, dataModel: memberDetail, dataIndex: memberDetailIndex });
    }

    formatModel(memberContact: MemberContactListResponse | MemberDetailResponse): MemberContactListResponse | MemberDetailResponse {
        if (memberContact['createdOn']) {
            memberContact['createdOn'] = moment(memberContact['createdOn']).format('MM/DD/YYYY');
        }
        if (memberContact['imageSource'] === null) {
            // assets/images/avatar-3.jpg
            memberContact['imageSource'] = 'assets/images/avatar-' + ((+memberContact['memberContactID']) % 5 + 1) + '.jpg';
        }

        return { ...memberContact };
    }
}
