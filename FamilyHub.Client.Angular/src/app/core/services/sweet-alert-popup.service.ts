import { Injectable } from '@angular/core';
import swal from 'sweetalert2';
import { sweetAlertType } from '../config/sweet-alert-type.config';


@Injectable()
export class SweetAlertPopupService {
    openBasicSwal(title: string, text: string) {
        swal({
            title: title,
            text: text
        }).catch(swal.noop);
    }

    openSuccessSwal(title: string, text: string) {
        swal({
            title: title,
            text: text,
            type: sweetAlertType.success
        }).catch(swal.noop);
    }

    openWarningSwal(title: string, text: string) {
        swal({
            title: title,
            text: text,
            type: sweetAlertType.warning
        }).catch(swal.noop);
    }

    openErrorSwal(title: string, text: string) {
        swal({
            title: title,
            text: text,
            type: sweetAlertType.error
        }).catch(swal.noop);
    }

    openInfoSwal(title: string, text: string) {
        swal({
            title: title,
            text: text,
            type: sweetAlertType.info
        }).catch(swal.noop);
    }

    openQuestionSwal(title: string, text: string) {
        swal({
            title: title,
            text: text,
            type: sweetAlertType.question
        }).catch(swal.noop);
    }

    openConfirmsSwal(title: string, text: string,
        confirmButtonText = 'YES', cancelButtonText = 'NO',
        confirmCallback?: any, cancelCallback?: any
    ) {
        swal({
            title: title,
            text: text,
            type: sweetAlertType.warning,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: confirmButtonText,
            cancelButtonText: cancelButtonText,
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger mr-sm'
        }).then((result) => {
            if (result.value) {
                confirmCallback();
            } else {
                cancelCallback();
            }
        });
    }
}
