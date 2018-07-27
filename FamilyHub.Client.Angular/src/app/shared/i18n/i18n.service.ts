import { Injectable, ApplicationRef } from '@angular/core';

import { IOption } from 'ng-select';
import { NgIOptionService } from '../services/ng-option.service';
import { LocalApiService } from '../services/local-api.service';
import { environment } from './../../../environments/environment';

import { Subject } from 'rxjs';



@Injectable()
export class I18nService {

    public languages: Array<IOption>;
    public state;
    public data: {};
    public currentLanguage: IOption;


    constructor(private localApiService: LocalApiService, private ref: ApplicationRef, private ngIOptionService: NgIOptionService) {
        this.state = new Subject();
        this.initLangulageOptions();
    }

    initLangulageOptions() {
        this.ngIOptionService.getLanguages().subscribe(result => {
            this.languages = result;

            this.initLanguage(`${environment.defaultLocale}` || 'us');
            this.fetch(this.currentLanguage.value);
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

    setLanguage(selctedlanguage: IOption) {
        this.currentLanguage = selctedlanguage;
        this.fetch(this.currentLanguage.value);
    }

    subscribe(sub: any, err: any) {
        return this.state.subscribe(sub, err);
    }

    public getTranslation(phrase: string): string {
        return this.data && this.data[phrase] ? this.data[phrase] : phrase;
    }
}
