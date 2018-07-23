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
import { MemberDetailRequest } from '../../../../core/models/member/member.model';

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
  currentDetailInfo: MemberDetailRequest = {
    memberContactID: 0,
    firstName: '',
    lastName: '',
    mobilePhone: '',
    homePhone: '',
    location: '',
    emailAddress: '',
    memberRelationshipID: '1'
  };

  /* ng-select */
  relationshipOption: Array<IOption> = [];
  selectedOption = null;

  private memberServiceSub: Subscription;
  constructor(
    public sharedConfig: SharedConfig,
    private memberService: MemberService,
    private ngIOptionService: NgIOptionService,
    private sharedService: SharedService
  ) { }

  ngOnInit() {
    this.memberServiceSub = this.memberService.memberDetailAction$.subscribe(
      (ele) => {
        if (ele.action === ActionState.CREATE) {
          this.loadNgSelectMemberRelationship();

          this.initFormValue(ele.dataModel);

          // this.memberDetailForm.form.patchValue({
          //   firstName: this.currentDetailInfo.firstName,
          //   lastName: this.currentDetailInfo.lastName,
          // });
        }
      }
    );
  }

  loadNgSelectMemberRelationship() {
    this.ngIOptionService.loadIOptionMembersRelationship()
    .subscribe((response) => {
      if (response.message === ResponseMessage.Success) {
        this.relationshipOption = response.model;

        this.sharedService.openModalAnimation('memberDetailPopupTemplateForm');
      } else {
        this.sharedService.openErrorSwal('Oops!', 'Something wrong with the server!');
      }
    });
  }

  initFormValue(memberDetail: MemberDetailRequest) {
    if (memberDetail !== null) {
      if (memberDetail['mobilePhone'] && memberDetail.mobilePhone.length === 10) {
        const conformedMobilePhone = conformToMask(
          memberDetail.mobilePhone,
          this.sharedConfig.getMaskConfig().maskUsMobile,
          { guide: false }
        );
        memberDetail.mobilePhone = conformedMobilePhone.conformedValue;
      }

      if (memberDetail['homePhone'] && memberDetail.homePhone.length === 10) {
        const conformedHomePhone = conformToMask(
          memberDetail.homePhone,
          this.sharedConfig.getMaskConfig().maskUsMobile,
          { guide: false }
        );
        memberDetail.homePhone = conformedHomePhone.conformedValue;
      }
      console.log('memberDetail:', memberDetail);
      this.currentDetailInfo = memberDetail;

      this.memberDetailForm.form.setValue({
        memberContactID: this.currentDetailInfo.memberContactID,
        firstName: this.currentDetailInfo.firstName,
        lastName: this.currentDetailInfo.lastName,
        mobilePhone: this.currentDetailInfo.mobilePhone,
        homePhone: this.currentDetailInfo.homePhone,
        location: this.currentDetailInfo.location,
        email: this.currentDetailInfo.emailAddress,
        memberRelationshipID: this.currentDetailInfo.memberRelationshipID
      });
    }
  }

  onSubmit(form: NgForm) {
    console.log('form.value:', form.value);
    // this.currentDetailInfo = form.value;
    console.log('this.currentDetailInfo:', this.currentDetailInfo);

    // this.sharedService.closeModalAnimation('memberDetailPopupTemplateForm');
    // this.sharedService.openSuccessSwal('Hooray', 'Saved Successfully!');
    this.memberDetailForm.reset();
  }

  closeModal() {
    this.sharedService.closeModalAnimation('memberDetailPopupTemplateForm');
  }

  ngOnDestroy() {
    this.memberServiceSub.unsubscribe();
  }
}
