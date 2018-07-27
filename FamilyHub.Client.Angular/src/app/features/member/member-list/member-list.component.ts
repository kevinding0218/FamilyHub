

import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Http } from '@angular/http';
import { IOption } from 'ng-select';

import { NgIOptionService } from '../../../core/services/ng-option.service';
import { MemberService } from '../../../core/services/member.service';
import { SharedService } from '../../../shared/services/shared.service';

import { ActionState } from '../../../core/config/action.config';
import { MemberDetailRequest } from '../../../core/models';
import { ResponseMessage } from '../../../core/config/api-response.config';


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

  public relationshipOptions: Array<IOption> = [];
  public memberDetailVideMode: string;
  public selectedMemberDetail: MemberDetailRequest = {} as any;

  // @Input('modalDefault') modalDefault: any;

  constructor(
    private ngIOptionService: NgIOptionService,
    private sharedService: SharedService,
    private memberService: MemberService,
    public http: Http) { }

  ngOnInit() {
    this.loadNgSelectMemberRelationship();

    this.http.get(`assets/data/crm-contact.json`)
      .subscribe((data) => {
        this.data = data.json();
      });
  }

  loadNgSelectMemberRelationship() {
    this.ngIOptionService.loadIOptionMembersRelationship()
    .subscribe((response) => {
      if (response.message === ResponseMessage.Success) {
        this.relationshipOptions = response.model;
      }
    });
  }

  openMemberDetailModal(action: string) {
    if (action === ActionState.CREATE) {
      const createMemberDetail: MemberDetailRequest = {
        memberContactID: 0,
        firstName: '',
        lastName: '',
        mobilePhone: '',
        homePhone: '',
        location: '',
        emailAddress: '',
        memberRelationshipID: '1'
      };

      this.selectedMemberDetail = createMemberDetail;
    } else if (action === ActionState.UPDATE) {
      const updateMemberDetail: MemberDetailRequest = {
        memberContactID: 0,
        firstName: 'Ran',
        lastName: 'Ding',
        mobilePhone: '1234567890',
        homePhone: '',
        location: 'Atlanta',
        emailAddress: '123@123.com',
        memberRelationshipID: '1'
      };

      this.selectedMemberDetail = updateMemberDetail;
    }
    this.memberDetailVideMode = action;
    // this.sharedService.openModalAnimation('memberDetailPopupTemplateForm');
    this.sharedService.openModalAnimation('memberDetailPopupReactiveForm');
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
}
