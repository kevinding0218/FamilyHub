import { NgModule, Optional, SkipSelf } from '@angular/core';

import { apiServices } from './services';
import { throwIfAlreadyLoaded } from './guards/module-import.guard';

@NgModule({
    providers: [
        ...apiServices
    ]
})

export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
      }
}
