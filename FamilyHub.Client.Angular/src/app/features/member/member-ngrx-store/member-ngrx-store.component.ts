import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { IOption } from 'ng-select';

import * as fromStore from '../../../core/store/member/member.reducer';
import * as fromSelector from '../../../core/store/member/member.selectors';

import { SharedService } from '../../../shared/services/shared.service';
import { NgIOptionService } from '../../../shared/services/ng-option.service';
import { MemberContactListResponse, MemberDetailRequest } from '../../../core/services/member.service';
import { ResponseMessage } from '../../../core/services/response extension/api-response.config';
import { ActionState } from '../../../shared/services/action.config';

@Component({
  selector: 'app-member-ngrx-store',
  templateUrl: './member-ngrx-store.component.html'
})
export class MemberNgrxStoreComponent implements OnInit {
  public selectedMember: MemberContactListResponse;
  public selectedMembers: Array<MemberContactListResponse>;
  public rowData: Array<MemberContactListResponse>;

  public relationshipOptions: Array<IOption> = [];
  public memberDetailViewMode: string;
  public selectedMemberDetail: MemberDetailRequest = {} as any;
  public selectedMemberDetailIndex: number;

  columnDefs: any[];

  constructor(
    private ngIOptionService: NgIOptionService,
    private sharedService: SharedService,
    private store: Store<fromStore.MemberState>
  ) { }

  ngOnInit() {
    this.loadNgSelectMemberRelationship();

    this.columnDefs = [
      { field: 'fullName', header: 'Full Name' },
      { field: 'contactPhone', header: 'Contact Phone' },
      { field: 'emailAddress', header: 'Email' },
      { field: 'location', header: 'Location' },
      { field: 'memberRelationshipName', header: 'Relationship' },
      { field: 'createdOn', header: 'Created Date' }
    ];

    this.store.select(fromSelector.getMemberList)
      .subscribe((selectorState) => {
        console.log('selectorState: ', selectorState);
        this.rowData = selectorState;
      });
  }

  loadNgSelectMemberRelationship(): void {
    this.ngIOptionService.loadIOptionMembersRelationship()
      .subscribe((response) => {
        if (response.message === ResponseMessage.Success) {
          this.relationshipOptions = response.model;
        }
      });
  }

  openMemberDetailModal(action: string): void {
    this.memberDetailViewMode = action;
    // this.sharedService.openModalAnimation('memberDetailPopupTemplateForm');
    this.sharedService.openModalAnimation('memberDetailPopupReactiveForm');
  }

  addMemberDetail(): void {
    this.selectedMemberDetail = {
      memberContactID: 0,
      firstName: '',
      lastName: '',
      mobilePhone: '',
      homePhone: '',
      location: '',
      emailAddress: '',
      memberRelationshipID: '1'
    };

    this.openMemberDetailModal(ActionState.CREATE);
  }

  RedirectDocumentation() {
    window.open('https://www.primefaces.org/primeng/#/table/selection', '_blank');
  }

}
