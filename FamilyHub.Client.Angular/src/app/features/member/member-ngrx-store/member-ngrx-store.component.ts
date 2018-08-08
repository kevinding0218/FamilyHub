import { MemberState } from './../../../core/store/member/member.reducer';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { MemberService, MemberContactListResponse } from './../../../core/services/member.service';

@Component({
  selector: 'app-member-ngrx-store',
  templateUrl: './member-ngrx-store.component.html'
})
export class MemberNgrxStoreComponent implements OnInit {
  public selectedMember: MemberContactListResponse;
  public selectedMembers: Array<MemberContactListResponse>;
  public rowData: Array<MemberContactListResponse>;

  columnDefs: any[];

  constructor(
    private memberService: MemberService,
    private store: Store<MemberState>
  ) { }

  ngOnInit() {
    this.columnDefs = [
      { field: 'fullName', header: 'Full Name' },
      { field: 'contactPhone', header: 'Contact Phone' },
      { field: 'emailAddress', header: 'Email' },
      { field: 'location', header: 'Location' },
      { field: 'memberRelationshipName', header: 'Relationship' },
      { field: 'createdOn', header: 'Created Date' }
    ];

    this.memberService.listMemberContact(0)
      .subscribe((listResponse) => {
        console.log('listResponse:', listResponse);
        this.rowData = listResponse.model as Array<MemberContactListResponse>;
      });
  }

  RedirectDocumentation() {
    window.open('https://www.primefaces.org/primeng/#/table/selection', '_blank');
  }

}
