import { NgModule, Optional, SkipSelf } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import * as fromStore from './store';

import { throwIfAlreadyLoaded } from './guards/module-import.guard';

@NgModule({
    imports: [
        StoreModule.forRoot(fromStore.reducers, {
            metaReducers: fromStore.metaReducers
        })
        // EffectsModule.forRoot([...fromStore.effects, AppEffects])
    ]
})

export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
    }
}
