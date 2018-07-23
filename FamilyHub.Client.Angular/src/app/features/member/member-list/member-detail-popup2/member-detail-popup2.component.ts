import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';

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
  selector: 'app-member-detail-popup2',
  templateUrl: './member-detail-popup2.component.html',
  styleUrls: [
    '../../../../../assets/icon/icofont/css/icofont.scss'
  ]
})
export class MemberDetailPopup2Component implements OnInit, OnDestroy {
  memberDetailForm: FormGroup;

  private formMode = '';
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
    this.initFormControl();

    this.memberServiceSub = this.memberService.memberDetailAction$.subscribe(
      (ele) => {
        if (ele.action === ActionState.CREATE || ele.action === ActionState.UPDATE || ele.action === ActionState.READ) {
          this.formMode = ele.action;

          this.loadNgSelectMemberRelationship();

          this.initFormValue(ele.dataModel);
        }
      }
    );
  }

  loadNgSelectMemberRelationship() {
    this.ngIOptionService.loadIOptionMembersRelationship()
    .subscribe((response) => {
      if (response.message === ResponseMessage.Success) {
        this.relationshipOption = response.model;

        this.sharedService.openModalAnimation('memberDetailPopupReactiveForm');
      } else {
        this.sharedService.openErrorSwal('Oops!', 'Something wrong with the server!');
      }
    });
  }

  private initFormControl() {
    this.memberDetailForm = new FormGroup({
      'firstName': new FormControl(this.currentDetailInfo.firstName, Validators.required),
      'lastName': new FormControl(this.currentDetailInfo.lastName, Validators.required),
      'mobilePhone': new FormControl(this.currentDetailInfo.mobilePhone),
      'homePhone': new FormControl(this.currentDetailInfo.homePhone),
      'location': new FormControl(this.currentDetailInfo.location),
      'emailAddress': new FormControl(this.currentDetailInfo.emailAddress),
      'memberRelationshipID': new FormControl(this.currentDetailInfo.memberRelationshipID, Validators.required)
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

      this.currentDetailInfo = memberDetail;
    }
  }

  onSubmit() {
    console.log('this.memberDetailForm:', this.memberDetailForm);
    console.log('this.currentDetailInfo:', this.currentDetailInfo);

    // this.sharedService.closeModalAnimation('memberDetailPopupReactiveForm');
    // this.sharedService.openSuccessSwal('Hooray', 'Saved Successfully!');
  }

  closeModal() {
    this.sharedService.closeModalAnimation('memberDetailPopupReactiveForm');
  }

  ngOnDestroy() {
    this.memberServiceSub.unsubscribe();
  }
}
