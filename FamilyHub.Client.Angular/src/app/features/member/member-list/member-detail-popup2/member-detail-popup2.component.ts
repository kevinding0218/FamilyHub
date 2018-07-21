import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';

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
  selector: 'app-member-detail-popup2',
  templateUrl: './member-detail-popup2.component.html'
})
export class MemberDetailPopup2Component implements OnInit, OnDestroy {
  memberDetailForm: FormGroup;

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

                this.sharedService.openModalAnimation('memberDetailPopup2');
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
  }

  closeModal() {
    this.sharedService.closeModalAnimation('memberDetailPopup2');
  }

  ngOnDestroy() {
    this.memberServiceSub.unsubscribe();
  }
}
