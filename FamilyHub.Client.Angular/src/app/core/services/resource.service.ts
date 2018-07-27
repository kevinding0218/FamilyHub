import { Observable } from 'rxjs/Observable';
import { environment } from './../../../environments/environment';
// tslint:disable-next-line:import-blacklist
import 'rxjs/Rx';
import { HttpClient } from '@angular/common/http';


export class Resource {
    id: number;
    parentId?: number;
}

export interface Serializer {
    fromJson(json: any): Resource;
    toJson(resource: Resource): any;
}

export interface QueryBuilder {
    toQueryMap: () => Map<string, string>;
    toQueryString: () => string;
}

export class QueryOptions implements QueryBuilder {
    public pageNumber: number;
    public pageSize: number;

    constructor() {
        this.pageNumber = 1;
        this.pageSize = 10000;
    }

    toQueryMap() {
        const queryMap = new Map<string, string>();
        queryMap.set('pageNumber', `${this.pageNumber}`);
        queryMap.set('pageSize', `${this.pageSize}`);

        return queryMap;
    }

    toQueryString() {
        let queryString = '';
        this.toQueryMap().forEach((value: string, key: string) => {
            queryString = queryString.concat(`${key}=${value}&`);
        });

        return queryString.substring(0, queryString.length - 1);
    }
}

export class ResourceService<T extends Resource> {
    constructor(
        private httpClient: HttpClient,
        private url: string = `${environment.REMOTE_API_URL}`,
        private endpoint: string,
        private serializer: Serializer) { }

    public create(apiMethod: string, item: T): Observable<T> {
        return this.httpClient
            .post<T>(`${this.url}/${this.endpoint}/${apiMethod}`, this.serializer.toJson(item))
            .map(data => this.serializer.fromJson(data) as T);
    }

    public update(apiMethod: string, item: T): Observable<T> {
        return this.httpClient
            .put<T>(`${this.url}/${this.endpoint}/${apiMethod}/${item.id}`,
                this.serializer.toJson(item))
            .map(data => this.serializer.fromJson(data) as T);
    }

    read(apiMethod: string, id: number): Observable<T> {
        return this.httpClient
            .get(`${this.url}/${this.endpoint}/${apiMethod}/${id}`)
            .map((data: any) => this.serializer.fromJson(data) as T);
    }

    list(apiMethod: string, queryOptions: QueryOptions): Observable<T[]> {
        return this.httpClient
            .get(`${this.url}/${this.endpoint}/${apiMethod}?${queryOptions.toQueryString()}`)
            .map((data: any) => this.convertData(data.items));
    }

    delete(apiMethod: string, id: number) {
        return this.httpClient
            .delete(`${this.url}/${this.endpoint}/${apiMethod}/${id}`);
    }

    private convertData(data: any): T[] {
        return data.map(item => this.serializer.fromJson(item));
    }
}

