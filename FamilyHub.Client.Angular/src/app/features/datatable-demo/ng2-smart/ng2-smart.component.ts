import { MemberContactListResponse, MemberService } from '../../../core/services/member.service';
import { Component, OnInit } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';

@Component({
  selector: 'app-ng2-smart',
  templateUrl: './ng2-smart.component.html',
  styleUrls: ['./ng2-smart.component.scss']
})
export class Ng2SmartComponent implements OnInit {
  source: LocalDataSource;

  settings = {
    columns: {
      fullName: {
        title: 'Full Name',
        type: 'string',
      },
      contactPhone: {
        title: 'Contact Phone',
        type: 'string',
      },
      emailAddress: {
        title: 'Email',
        type: 'string',
      },
      location: {
        title: 'Location',
        type: 'string',
      },
      memberRelationshipName: {
        title: 'Relationship',
        type: 'string',
      },
      createdOn: {
        title: 'Created Date',
        type: 'string',
      },
    },
  };

  constructor(private memberService: MemberService) { }

  ngOnInit() {
    this.source = new LocalDataSource();

    this.memberService.listMemberContact(0)
      .subscribe((listResponse) => {
        console.log('listResponse:', listResponse);
        this.source.load(listResponse.model as Array<MemberContactListResponse>);
      });
  }

  RedirectDocumentation() {
    window.open('https://akveo.github.io/ng2-smart-table/#/examples/various', '_blank');
  }
}
