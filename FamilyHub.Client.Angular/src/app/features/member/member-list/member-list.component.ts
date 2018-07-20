import { Component, Input, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { IOption } from 'ng-select';

import { SweetAlertPopupService } from './../../../core/services/sweet-alert-popup.service';
import { NgIOptionService } from '../../../core/services/ng-option.service';
import { IResponseMessage } from '../../../core/config/api-response.config';



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

  /* Text Mask */
  public maskUsMobile = ['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/];

  /* ng-select */
  simpleOption: Array<IOption> = [
    { value: '0', label: 'Alabama' },
    { value: '1', label: 'Wyoming' },
    { value: '2', label: 'Coming' },
    { value: '3', label: 'Henry Die' },
    { value: '4', label: 'John Doe' }
  ];
  selectedOption = null;

  @Input('modalDefault') modalDefault: any;

  constructor(
    private ngIOptionService: NgIOptionService,
    private sapopup: SweetAlertPopupService,
    public http: Http) { }

  ngOnInit() {
    this.http.get(`assets/data/crm-contact.json`)
      .subscribe((data) => {
        this.data = data.json();
      });
  }

  openMyModal(event) {
    document.querySelector('#' + event).classList.add('md-show');

    this.ngIOptionService.loadIOptionMembersRelationship()
      .subscribe((response) => {
        if (response.message !== IResponseMessage.Success) {
          // this.sapopup.openSuccessSwal('Hooray', 'Get Success Response!');
          this.simpleOption = response.model;
        } else {
          // this.closeMyModal();
          this.sapopup.openErrorSwal('Oops!', 'Something wrong with the server!');
        }
      });
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

  closeMyModal(event) {
    console.log('event:', event);
    ((event.target.parentElement.parentElement).parentElement).classList.remove('md-show');
  }
}
