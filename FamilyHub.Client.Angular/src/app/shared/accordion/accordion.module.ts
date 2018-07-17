import { NgModule } from '@angular/core';

import { AccordionLinkDirective } from './accordionlink.directive';
import { AccordionAnchorDirective } from './accordionanchor.directive';
import { AccordionDirective } from './accordion.directive';

@NgModule({
    declarations: [
        AccordionDirective,
        AccordionAnchorDirective,
        AccordionLinkDirective
    ],
    exports: [
        AccordionDirective,
        AccordionAnchorDirective,
        AccordionLinkDirective
    ]
})
export class AccordionDirectiveModule {}