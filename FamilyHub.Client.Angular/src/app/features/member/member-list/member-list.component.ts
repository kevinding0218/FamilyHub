
import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Http } from '@angular/http';
import { IOption } from 'ng-select';

import { MemberService } from '../../../core/services/member.service';
import { SharedService } from '../../../shared/services/shared.service';

import { ActionState } from '../../../core/config/action.config';
import { MemberContactCreateRequest } from '../../../core/models';


@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: [
    '../../../../assets/icon/icofont/css/icofont.scss'
  ]
})
export class MemberListComponent implements OnInit {

  public data: any;
  public rowsOnPage = 10;
  public filterQuery = '';
  public sortBy = '';
  public sortOrder = 'desc';

  public userName: string;
  public userID: string;
  public userProPic: string;
  public userEmail: string;
  public userPosition: string;
  public userOffice: string;
  public userAge: number;
  public userContact: string;
  public userDate: string;

  /* ng-select */
  simpleOption: Array<IOption> = [];
  selectedOption = null;

  // @Input('modalDefault') modalDefault: any;

  constructor(
    private sharedService: SharedService,
    private memberService: MemberService,
    public http: Http) { }

  ngOnInit() {
    this.http.get(`assets/data/crm-contact.json`)
      .subscribe((data) => {
        this.data = data.json();
      });
  }

  openMemberDetailModal(action: string) {
    if (action === ActionState.CREATE) {
      const newMemberDetail: MemberContactCreateRequest = {} as any;
      // const newMemberDetail: MemberContactCreateRequest = {
      //   firstName: 'Ran',
      //   lastName: 'Ding',
      //   mobilePhone: '1234567890',
      //   homePhone: '',
      //   location: 'ATLANTA GA',
      //   emailAddress: '123@123.com',
      //   memberRelationshipID: '1'
      // };

      this.memberService.preloadMemberDetail(newMemberDetail);
    }
  }

  openMyModalData(event) {
    this.userName = this.data[event]['name'];
    this.userID = this.data[event]['id'];
    this.userProPic = this.data[event]['image'];
    this.userEmail = this.data[event]['email'];
    this.userPosition = this.data[event]['position'];
    this.userOffice = this.data[event]['office'];
    this.userAge = this.data[event]['age'];
    this.userContact = this.data[event]['phone_no'];
    this.userDate = this.data[event]['date'];
  }

  saveNewMember() {
    this.sharedService.closeModalAnimation('memberDetailPopup');
    this.sharedService.openSuccessSwal('Hooray', 'Saved Successfully!');
  }

  closeNewMemberModal() {
    this.sharedService.closeModalAnimation('memberDetailPopup');
  }
}
