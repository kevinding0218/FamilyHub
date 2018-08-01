import { NgModule, NO_ERRORS_SCHEMA, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AccordionDirectiveModule } from './accordion/accordion.module';
import { FamilyHubLayoutModule } from './layout/layout.module';
import { ToggleFullScreenDirective } from './fullscreen/toggle-fullscreen.directive';
import { PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { CardComponent } from './card/card.component';
import { CardToggleDirective } from './card/card-toggle.directive';
import { ModalBasicComponent } from './modal-basic/modal-basic.component';
import { ModalAnimationComponent } from './modal-animation/modal-animation.component';
import { SpinnerModule } from './spinner/spinner.module';
import { ClickOutsideModule } from 'ng-click-outside';
import { PipesModule } from './pipes/pipes.module';
import { I18nModule } from './i18n/i18n.module';

import { TokenInterceptor } from './services/http-interceptor/token.interceptor';
import { LoggingInterceptor } from './services/http-interceptor/logging.interceptor';
import { sharedServices } from './services';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    ToggleFullScreenDirective,
    CardToggleDirective,
    CardComponent,
    ModalBasicComponent,
    ModalAnimationComponent
  ],
  imports: [
    CommonModule,
    NgbModule.forRoot(),
    HttpClientModule,
    AccordionDirectiveModule,
    FamilyHubLayoutModule,
    PerfectScrollbarModule,
    ClickOutsideModule,
    SpinnerModule,
    I18nModule,
  ],
  exports: [
    NgbModule,
    ToggleFullScreenDirective,
    AccordionDirectiveModule,
    CardToggleDirective,
    HttpClientModule,
    FamilyHubLayoutModule,
    PerfectScrollbarModule,
    CardComponent,
    ModalBasicComponent,
    ModalAnimationComponent,
    SpinnerModule,
    ClickOutsideModule,
    PipesModule,
  ],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoggingInterceptor,
      multi: true
    },
    ...sharedServices
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class SharedModule {

}
