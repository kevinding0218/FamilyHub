import { IOption } from 'ng-select';
import { NgIOptionService } from '../../core/services/ng-option.service';
import { LocalApiService } from '../../core/services/local-api.service';
import { familyHubConfig } from '../../core/config/familyhub.config';
import { Injectable, ApplicationRef } from '@angular/core';

import { Subject } from 'rxjs';



@Injectable()
export class I18nService {

    public languages: Array<IOption>;
    public state;
    public data: {};
    public currentLanguage: IOption;


    constructor(private localApiService: LocalApiService, private ref: ApplicationRef, private ngIOptionService: NgIOptionService) {
        this.state = new Subject();
        this.getAllLanguages();

        this.initLanguage(familyHubConfig.defaultLocale || 'us');
        this.fetch(this.currentLanguage.value);
    }

    getAllLanguages() {
        this.ngIOptionService.getLanguages().subscribe(result => {
            this.languages = result;
        });
    }

    private fetch(locale: any) {
        this.localApiService.fetchLocalResource(`/langs/${locale}.json`)
            .subscribe((data: any) => {
                this.data = data;
                this.state.next(data);
                this.ref.tick();
            });
    }

    private initLanguage(locale: string) {
        const language = this.languages.find((it) => {
            return it.value === locale;
        });
        if (language) {
            this.currentLanguage = language;
        } else {
            throw new Error(`Incorrect locale used for I18nService: ${locale}`);

        }
    }

    setLanguage(language) {
        this.currentLanguage = language;
        this.fetch(language.key);
    }

    subscribe(sub: any, err: any) {
        return this.state.subscribe(sub, err);
    }

    public getTranslation(phrase: string): string {
        return this.data && this.data[phrase] ? this.data[phrase] : phrase;
    }
}
