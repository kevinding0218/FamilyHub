import { Component, OnInit, ViewChild, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { NgForm } from '@angular/forms';

import { IOption } from 'ng-select';
import { conformToMask } from 'angular2-text-mask';

import { Subscription } from 'rxjs/Subscription';
import { SharedConfig } from '../../../../shared/utils/shared.config';
import { SharedService } from '../../../../shared/services/shared.service';
import { MemberService, MemberDetailRequest } from '../../../../core/services/member.service';


@Component({
  selector: 'app-member-detail-popup',
  templateUrl: './member-detail-popup.component.html',
  styleUrls: [
    '../../../../../assets/icon/icofont/css/icofont.scss',
    './test.scss'
  ]
})
export class MemberDetailPopupComponent implements OnChanges, OnInit {
  @ViewChild('f') memberDetailForm: NgForm;
  @Input() currentDetailInfo: MemberDetailRequest;
  @Input() viewMode: string;
  @Input() relationshipOptions: Array<IOption> = [];

  private memberServiceSub: Subscription;
  constructor(
    public sharedConfig: SharedConfig,
    private sharedService: SharedService,
    private memberService: MemberService
  ) { }

  ngOnInit() { }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['currentDetailInfo']) {
      // const detail: SimpleChange = changes.currentDetailInfo;
      // if (detail.previousValue !== undefined) {
      //   console.log('prev detail: ', detail.previousValue);
      // }
      // if (detail.currentValue !== undefined) {
      //   console.log('current detail: ', detail.currentValue);
      // }

      this.initFormValue();
    }
  }

  initFormValue() {
    if (this.currentDetailInfo['memberContactID'] !== undefined) {
      if (this.currentDetailInfo['mobilePhone'] && this.currentDetailInfo.mobilePhone.length === 10) {
        const conformedMobilePhone = conformToMask(
          this.currentDetailInfo.mobilePhone,
          this.sharedConfig.getMaskConfig().maskUsMobile,
          { guide: false }
        );
        this.currentDetailInfo.mobilePhone = conformedMobilePhone.conformedValue;
      }

      if (this.currentDetailInfo['homePhone'] && this.currentDetailInfo.homePhone.length === 10) {
        const conformedHomePhone = conformToMask(
          this.currentDetailInfo.homePhone,
          this.sharedConfig.getMaskConfig().maskUsMobile,
          { guide: false }
        );
        this.currentDetailInfo.homePhone = conformedHomePhone.conformedValue;
      }
      console.log('memberDetail:', this.currentDetailInfo);

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
}
