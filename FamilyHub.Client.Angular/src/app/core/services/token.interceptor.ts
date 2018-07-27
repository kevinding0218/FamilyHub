import { Injectable, Injector } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import 'rxjs/add/operator/do';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(
        private injector: Injector
    ) { }
    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        // if (request.url.search('asset') !== 0 || request.url.search('login') !== 0) {
        //   // attach tcken
        //   return this.handleApiRequest(request, next);
        // } else {
        //   return next.handle(request);
        // }
        return next.handle(request);
    }



    handleApiRequest(request, next) {
        request = request.clone({
            setHeaders: {
                Authorization: `Bearer 123456`
            }
        });

        const handler = next.handle(request).pipe(
            catchError((error, caught) => {
                if (error.status === 401 || error.status === 403) {

                    //   this.store.dispatch(new fromAuth.LogoutAction());
                    return throwError(error);
                } else {
                    return throwError(error);
                }
            })
        );

        return handler;
    }
}
