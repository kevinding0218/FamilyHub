import { MemberContactListResponse } from '../../../core/services/member.service';
import { Subscription } from 'rxjs/Subscription';
import { Component, Input, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { IOption } from 'ng-select';

import { MemberService, MemberDetailRequest } from '../../../core/services/member.service';
import { SharedService } from '../../../shared/services/shared.service';
import { NgIOptionService } from '../../../shared/services/ng-option.service';

import { TableConfig } from '../../../shared/utils/table.config';
import { ActionState } from '../../../shared/services/action.config';
import { ResponseMessage } from '../../../core/services/response extension/api-response.config';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: [
    '../../../../assets/icon/icofont/css/icofont.scss'
  ]
})

export class MemberListComponent implements OnInit, OnDestroy {
  public tableConfig: TableConfig<MemberContactListResponse> = {
    data: [],
    rowsOnPage: 10,
    filterQuery: '',
    sortBy: '',
    sortOrder: 'desc'
  };

  public relationshipOptions: Array<IOption> = [];
  public memberDetailViewMode: string;
  public selectedMemberDetail: MemberDetailRequest = {} as any;
  public selectedMemberDetailIndex: number;
  private detailSubscription: Subscription;

  constructor(
    private ngIOptionService: NgIOptionService,
    private sharedService: SharedService,
    private memberService: MemberService) { }

  ngOnInit() {
    this.loadNgSelectMemberRelationship();

    this.memberService.listMemberContact(0)
      .subscribe((listResponse) => {
        console.log('listResponse:', listResponse);
        this.tableConfig.data = listResponse.model as Array<MemberContactListResponse>;
      });

    this.detailSubscription = this.memberService.memberDetailAction$
      .subscribe(
        (message) => {
          switch (message.action) {
            case ActionState.CREATE:
              console.log('After Create:', message.dataModel);
              console.log('old this.tableConfig.data:', this.tableConfig.data);
              this.tableConfig.data.push(<MemberContactListResponse>message.dataModel);
              console.log('new this.tableConfig.data:', this.tableConfig.data);
              setTimeout(() => {
                this.sharedService.openSuccessSwal('Hooray',
                  'New contact ' + message.dataModel.fullName + ' has been created successfully!');
              }, 800);
              break;
            case ActionState.UPDATE:
              console.log('After Update:', message.dataModel);
              this.tableConfig.data[message.dataIndex] = <MemberContactListResponse>message.dataModel;
              setTimeout(() => {
                this.sharedService.openSuccessSwal('Hooray',
                  'Contact ' + message.dataModel.fullName + ' has been updated successfully!');
              }, 800);
              break;
            case ActionState.DELETE:
              console.log('After Delete:', message.dataIndex);
              this.tableConfig.data.splice(message.dataIndex, 1);

              setTimeout(() => {
                this.sharedService.openSuccessSwal('Hooray',
                  'Contact ' + message.dataModel.fullName + ' has been removed!');
              }, 800);
              break;
          }
        }
      );
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

  gridItemToDetail(selectedItem: MemberContactListResponse): void {
    this.selectedMemberDetail = {
      memberContactID: selectedItem.memberContactID,
      firstName: selectedItem.firstName,
      lastName: selectedItem.lastName,
      mobilePhone: selectedItem.mobilePhone,
      homePhone: selectedItem.homePhone,
      location: selectedItem.location,
      emailAddress: selectedItem.emailAddress,
      memberRelationshipID: selectedItem.memberRelationshipID
    };
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

  viewMemberDetail(selectedItem: MemberContactListResponse, selectedIndex: number): void {
    this.gridItemToDetail(selectedItem);

    this.openMemberDetailModal(ActionState.READ);
  }

  updateMemberDetail(selectedItem: MemberContactListResponse, selectedIndex: number): void {
    this.gridItemToDetail(selectedItem);

    this.openMemberDetailModal(ActionState.UPDATE);
  }

  deleteMemberDetail(selectedItem: MemberContactListResponse, selectedIndex: number): void {
    this.sharedService.openConfirmsSwal('Are you sure to remove ' + selectedItem.fullName + ' ?', 'You wont be able to revert',
      'Yes, remove it!', 'Let me reconsider..', () => this.continueDelete(selectedItem, selectedIndex), () => { }
    );
  }

  continueDelete(selectedItem: MemberContactListResponse, selectedIndex: number) {
    this.memberService.deleteMemberDetail(selectedItem.memberContactID).subscribe(result => {
      if (result.message === ResponseMessage.Success) {
        this.memberService.afterDeleteMemberDetail(selectedItem, selectedIndex);
      }
    });
  }

  ngOnDestroy() {
    this.detailSubscription.unsubscribe();
  }
}
