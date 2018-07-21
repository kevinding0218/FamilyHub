import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import swal from 'sweetalert2';

export const enum sweetAlertType {
    success = 'success',
    error = 'error',
    warning = 'warning',
    info = 'info',
    question = 'question'
}

@Injectable()
export class SharedService {
    // #region Subject
    liveNotification$ = new Subject<string>();
    toggleLiveNotification$ = new Subject();

    modalAnimationAction$ = new Subject<{ action: string, modalID: string, dataModel?: any }>();
    // #endregion

    // #region ModalAnimation
    openModalAnimation(modalID: string): void {
        this.modalAnimationAction$.next({ action: 'open', modalID });
    }

    openModalAnimationWithModel(modalID: string, dataModel: any): void {
        this.modalAnimationAction$.next({ action: 'open', modalID, 'dataModel': dataModel });
    }

    closeModalAnimation(modalID: string): void {
        this.modalAnimationAction$.next({ action: 'close', modalID });
    }
    // #endregion

    // #region Sweet-Alert
    openBasicSwal(title: string, text: string): void {
        setTimeout(() => {
            swal({
                title: title,
                text: text,
                allowOutsideClick: false,
                timer: 3000
            }).catch(swal.noop);
        }, 500);
    }

    openSuccessSwal(title: string, text: string): void {
        setTimeout(() => {
            swal({
                title: title,
                text: text,
                type: sweetAlertType.success,
                allowOutsideClick: false,
                timer: 3000
            }).catch(swal.noop);
        }, 500);
    }

    openWarningSwal(title: string, text: string): void {
        setTimeout(() => {
            swal({
                title: title,
                text: text,
                type: sweetAlertType.warning,
                allowOutsideClick: false,
                timer: 3000
            }).catch(swal.noop);
        }, 500);
    }

    openErrorSwal(title: string, text: string): void {
        setTimeout(() => {
            swal({
                title: title,
                text: text,
                type: sweetAlertType.error,
                allowOutsideClick: false,
                timer: 3000
            }).catch(swal.noop);
        }, 500);
    }

    openInfoSwal(title: string, text: string): void {
        setTimeout(() => {
            swal({
                title: title,
                text: text,
                type: sweetAlertType.info,
                allowOutsideClick: false,
                timer: 3000
            }).catch(swal.noop);
        }, 500);
    }

    openQuestionSwal(title: string, text: string): void {
        setTimeout(() => {
            swal({
                title: title,
                text: text,
                type: sweetAlertType.question,
                allowOutsideClick: false,
                timer: 3000
            }).catch(swal.noop);
        }, 500);
    }

    openConfirmsSwal(title: string, text: string,
        confirmButtonText = 'YES', cancelButtonText = 'NO',
        confirmCallback?: any, cancelCallback?: any
    ): void {
        setTimeout(() => {
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
                cancelButtonClass: 'btn btn-danger mr-sm',
                allowOutsideClick: false,
                timer: 3000
            }).then((result) => {
                if (result.value) {
                    confirmCallback();
                } else {
                    cancelCallback();
                }
            });
        }, 500);
    }
    // #endregion
}
