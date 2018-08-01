import { MemberService, MemberContactListResponse } from '../../../core/services/member.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-ag-grid',
  templateUrl: './ag-grid.component.html',
  styleUrls: ['./ag-grid.component.scss']
})
export class AgGridComponent implements OnInit {
  public gridApi;
  public gridColumnApi;
  public rowData: Array<MemberContactListResponse>;

  public columnDefs;
  public rowSelection;
  public isFullWidthCell;

  constructor(private memberService: MemberService) { }

  ngOnInit() {
    this.columnDefs = [
      {headerName: 'Full Name', field: 'fullName'},
      {headerName: 'Contact Phone', field: 'contactPhone'},
      {headerName: 'Email', field: 'emailAddress', width: 250},
      {headerName: 'Location', field: 'location'},
      {headerName: 'Relationship', field: 'memberRelationshipName'},
      {headerName: 'Created Date', field: 'createdOn'}
    ];

    this.rowSelection = 'single';

    this.isFullWidthCell = function (rowNode) {
      return rowNode.data.fullWidth;
    };
  }

  onGridReady(params) {
    console.log('onGridReady params:', params);
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;

    this.memberService.listMemberContact(0)
      .subscribe((listResponse) => {
        console.log('listResponse:', listResponse);
        this.rowData = listResponse.model as Array<MemberContactListResponse>;
      });
  }

  RedirectDocumentation() {
    window.open('https://www.ag-grid.com/javascript-grid-themes/material-theme.php', '_blank');
  }
}
