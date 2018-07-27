import { Component, OnInit } from '@angular/core';

import { IOption } from 'ng-select';
import { I18nService } from '../i18n.service';
import { NgIOptionService } from '../../../core/services/ng-option.service';

@Component({
    selector: 'app-language-selector',
    templateUrl: './language-selector.component.html',
})
export class LanguageSelectorComponent implements OnInit {

    languages: Array<IOption>;
    selectedLanguage = 'us';

    constructor(private i18n: I18nService, private ngIOptionService: NgIOptionService) {
    }

    ngOnInit() {
        this.ngIOptionService.getLanguages().subscribe(result => {
            this.languages = result;
        });
    }

    onSelected(option: IOption) {
        this.i18n.setLanguage(option);
    }
}
