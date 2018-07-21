export const enum ResponseMessage {
    Success = 'Success',
    Warning = 'Warning',
    Error = 'Error',
    Valid = 'Valid',
    Invalid = 'Invalid',
    NotAuthorized = 'NotAuthorized',
    Found = 'Found',
    NotFound = 'NotFound',
    Duplicate = 'Duplicate'
}

import { IOption } from 'ng-select';

export interface IResponse {
    message: string;
    didError: boolean;
    errorMessage: string;
}

export interface ISingleResponse extends IResponse {
    model: any;
}

export interface IOptionResponse extends IResponse {
    model: Array<IOption>;
}

export interface IListResponse extends IResponse {
    model: Array<any>;
}

export interface IPagedResponse extends IListResponse {
    itemsCount: number;
    pageCount: number;
}


