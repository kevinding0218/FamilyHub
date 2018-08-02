import { MemberService, MemberContactListResponse } from './../../../core/services/member.service';
import { Component, OnInit } from '@angular/core';
import { ContextMenuItem, GroupSettingsModel, EditSettingsModel } from '@syncfusion/ej2-ng-grids';

@Component({
  selector: 'app-ejs-datatable-demo',
  templateUrl: './ejs-datatable-demo.component.html',
  styleUrls: ['./ejs-datatable-demo.component.scss']
})
export class EjsDatatableDemoComponent implements OnInit {

  public data: Array<MemberContactListResponse>;
  public contextMenuItems: ContextMenuItem[];
  public editing: EditSettingsModel;

  constructor(private memberService: MemberService) {}

  ngOnInit(): void {
    this.memberService.listMemberContact(0)
      .subscribe((listResponse) => {
        console.log('listResponse:', listResponse);
        this.data = listResponse.model as Array<MemberContactListResponse>;
      });
    this.contextMenuItems = ['AutoFit', 'AutoFitAll', 'SortAscending', 'SortDescending',
      'Copy', 'Edit', 'Delete', 'Save', 'Cancel',
      'PdfExport', 'ExcelExport', 'CsvExport', 'FirstPage', 'PrevPage',
      'LastPage', 'NextPage'];
    this.editing = { allowDeleting: true, allowEditing: true };
  }

  RedirectDocumentation() {
    window.open('https://ej2.syncfusion.com/16.2.41/angular/demos/#/material/grid/over-view', '_blank');
  }
}
