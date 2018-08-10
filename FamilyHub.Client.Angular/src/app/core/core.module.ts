import { NgModule, Optional, SkipSelf } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import * as fromStore from './store/app.reducers';

import { throwIfAlreadyLoaded } from './guards/module-import.guard';

@NgModule({
    imports: [
        StoreModule.forRoot(fromStore.reducers, {
            metaReducers: fromStore.metaReducers
        }),
        StoreDevtoolsModule.instrument(),
        EffectsModule.forRoot([...fromStore.effects])
    ]
})

export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
    }
}
