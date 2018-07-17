import { NgModule, ModuleWithProviders, APP_INITIALIZER , Optional, SkipSelf } from '@angular/core';

import { services } from './services';
import { throwIfAlreadyLoaded } from './guards/module-import.guard';

@NgModule({
    providers: [
        ...services
    ]
})

export class CoreModule {
    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
      }
}