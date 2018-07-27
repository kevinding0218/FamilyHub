import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { apiServices } from './services';
import { throwIfAlreadyLoaded } from './guards/module-import.guard';
import { TokenInterceptor } from './services/token.interceptor';
import { LoggingInterceptor } from './services/logging.interceptor';

@NgModule({
    providers: [
        ...apiServices,
        { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true }
    ]
})

export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
    }
}
