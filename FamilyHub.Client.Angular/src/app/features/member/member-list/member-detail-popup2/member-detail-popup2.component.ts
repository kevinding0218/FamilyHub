import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { IOption } from 'ng-select';

import { conformToMask } from 'angular2-text-mask';
import { SharedConfig } from './../../../../shared/utils/shared.config';
import { SharedService } from '../../../../shared/services/shared.service';
import { MemberDetailRequest } from '../../../../core/models/member/member.model';

@Component({
  selector: 'app-member-detail-popup2',
  templateUrl: './member-detail-popup2.component.html',
  styleUrls: [
    '../../../../../assets/icon/icofont/css/icofont.scss'
  ]
})
export class MemberDetailPopup2Component implements OnInit, OnChanges {
  public memberDetailForm: FormGroup;
  @Input() currentDetailInfo: MemberDetailRequest;
  @Input() viewMode: string;
  @Input() relationshipOptions: Array<IOption> = [];

  constructor(
    public sharedConfig: SharedConfig,
    private sharedService: SharedService
  ) { }

  ngOnInit() {
    this.initFormControl();
   }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['currentDetailInfo']) {
      this.initFormValue();
    }
  }

  private initFormControl() {
    this.memberDetailForm = new FormGroup({
      'memberContactID': new FormControl(0),
      'firstName': new FormControl(null, Validators.required),
      'lastName': new FormControl(null, Validators.required),
      'mobilePhone': new FormControl(null),
      'homePhone': new FormControl(null),
      'location': new FormControl(null),
      'emailAddress': new FormControl(null),
      'memberRelationshipID': new FormControl('1', Validators.required)
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.memberDetailForm.controls; }

  private initFormValue() {
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

      this.memberDetailForm.setValue({
        'memberContactID': this.currentDetailInfo.memberContactID,
        'firstName': this.currentDetailInfo.firstName,
        'lastName': this.currentDetailInfo.lastName,
        'mobilePhone': this.currentDetailInfo.mobilePhone,
        'homePhone': this.currentDetailInfo.homePhone,
        'location': this.currentDetailInfo.location,
        'emailAddress': this.currentDetailInfo.emailAddress,
        'memberRelationshipID': this.currentDetailInfo.memberRelationshipID
      });
    }
  }

  onSubmit() {
    console.log('this.memberDetailForm:', this.memberDetailForm);
    console.log('this.currentDetailInfo:', this.currentDetailInfo);

    this.memberDetailForm.reset();

    // this.sharedService.closeModalAnimation('memberDetailPopupReactiveForm');
    // this.sharedService.openSuccessSwal('Hooray', 'Saved Successfully!');
  }

  closeModal() {
    this.sharedService.closeModalAnimation('memberDetailPopupReactiveForm');
  }
}
