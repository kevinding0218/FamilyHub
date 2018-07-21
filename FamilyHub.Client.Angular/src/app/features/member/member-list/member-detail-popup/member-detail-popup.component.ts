
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

import { IOption } from 'ng-select';
import { Subscription } from 'rxjs/Subscription';

import { conformToMask } from 'angular2-text-mask';
import { SharedConfig } from './../../../../shared/utils/shared.config';
import { SharedService } from '../../../../shared/services/shared.service';
import { NgIOptionService } from '../../../../core/services/ng-option.service';
import { ResponseMessage } from '../../../../core/config/api-response.config';
import { MemberService } from '../../../../core/services/member.service';
import { ActionState } from '../../../../core/config/action.config';
import { MemberContactCreateRequest } from '../../../../core/models/member/member.model';

@Component({
  selector: 'app-member-detail-popup',
  templateUrl: './member-detail-popup.component.html',
  styleUrls: [
    '../../../../../assets/icon/icofont/css/icofont.scss',
    './test.scss'
  ]
})
export class MemberDetailPopupComponent implements OnInit, OnDestroy {
  @ViewChild('f') memberDetailForm: NgForm;
  currentDetailInfo: MemberContactCreateRequest = {
    firstName: '',
    lastName: '',
    mobilePhone: '',
    homePhone: '',
    location: '',
    emailAddress: '',
    memberRelationshipID: '1'
  };
  loadModalContent: Boolean = false;
  /* ng-select */
  relationshipOption: Array<IOption> = [];
  selectedOption = null;

  maskedMobilePhone = '';
  maskedHomePhone = '';

  private memberServiceSub: Subscription;
  constructor(
    public sharedConfig: SharedConfig,
    private memberService: MemberService,
    private ngIOptionService: NgIOptionService,
    private sharedService: SharedService
  ) { }

  ngOnInit() {
    this.memberServiceSub = this.memberService.memberAction$.subscribe(
      (ele) => {
        if (ele.action === ActionState.PRELOAD) {
          this.ngIOptionService.loadIOptionMembersRelationship()
            .subscribe((response) => {
              if (response.message === ResponseMessage.Success) {
                this.relationshipOption = response.model;

                this.sharedService.openModalAnimation('memberDetailPopup');
              } else {
                this.sharedService.openErrorSwal('Oops!', 'Something wrong with the server!');
              }
            });

          const conformedMobilePhone = conformToMask(
            ele.dataModel.mobilePhone,
            this.sharedConfig.getMaskConfig().maskUsMobile,
            { guide: false }
          );
          this.maskedMobilePhone = conformedMobilePhone.conformedValue;

          const conformedHomePhone = conformToMask(
            ele.dataModel.homePhone,
            this.sharedConfig.getMaskConfig().maskUsMobile,
            { guide: false }
          );
          this.maskedHomePhone = conformedHomePhone.conformedValue;

          this.currentDetailInfo = ele.dataModel;

          this.memberDetailForm.form.setValue({
            firstName: this.currentDetailInfo.firstName,
            lastName: this.currentDetailInfo.lastName,
            mobilePhone: this.maskedMobilePhone,
            homePhone: this.maskedHomePhone,
            location: this.currentDetailInfo.location,
            email: this.currentDetailInfo.emailAddress,
            relationship: this.currentDetailInfo.memberRelationshipID
          });

          // this.memberDetailForm.form.patchValue({
          //   firstName: this.currentDetailInfo.firstName,
          //   lastName: this.currentDetailInfo.lastName,
          // });
        }
      }
    );
  }

  saveNewMember(form: NgForm) {
    console.log(form);

    this.currentDetailInfo.mobilePhone = this.maskedMobilePhone.replace(/\D+/g, '');
    this.currentDetailInfo.homePhone = this.maskedHomePhone.replace(/\D+/g, '');
    console.log('this.currentDetailInfo:', this.currentDetailInfo);

    // this.sharedService.closeModalAnimation('memberDetailPopup');
    // this.sharedService.openSuccessSwal('Hooray', 'Saved Successfully!');
    this.memberDetailForm.reset();
  }

  closeModal() {
    this.sharedService.closeModalAnimation('memberDetailPopup');
  }

  ngOnDestroy() {
    this.memberServiceSub.unsubscribe();
  }
}
