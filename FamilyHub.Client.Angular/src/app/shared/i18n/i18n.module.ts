import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { SelectModule } from 'ng-select';
import { LanguageSelectorComponent } from './language-selector/language-selector.component';
import { I18nPipe } from './i18n.pipe';
import { I18nService } from './i18n.service';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        SelectModule
    ],
    declarations: [
        LanguageSelectorComponent,
        I18nPipe
    ],
    exports: [LanguageSelectorComponent, I18nPipe],
    providers: [I18nService]

})
export class I18nModule { }
