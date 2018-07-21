import { Injectable } from '@angular/core';
import { createAutoCorrectedDatePipe, createNumberMask, emailMask } from 'text-mask-addons/dist/textMaskAddons';

const MASK_CONFIG: any = {
    maskUsMobile: ['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/],
    maskUsMobileCountryCode: ['+', '1', ' ', '(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/],
    maskDate: [/\d/, /\d/, '/', /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/],
    maskDateAuto: [/\d/, /\d/, '/', /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/],
    maskZipCode: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
    maskDatePipe: createAutoCorrectedDatePipe('dd/mm/yyyy'),
    maskUsAmount: createNumberMask({
        prefix: '$'
    }),
    maskUsAmountDecimal: createNumberMask({
        prefix: '',
        suffix: ' $',
        allowDecimal: true
    }),
    maskPercentage: createNumberMask({
        prefix: '',
        suffix: ' %',
        integerLimit: 2
    }),
    emailMask: emailMask
};

@Injectable()
export class SharedConfig {
    getMaskConfig(): any {
        return MASK_CONFIG;
    }
}
