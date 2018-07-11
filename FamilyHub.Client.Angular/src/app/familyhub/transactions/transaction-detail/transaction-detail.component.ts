import { ModalDirective } from 'ngx-bootstrap';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'sa-transaction-detail',
  templateUrl: './transaction-detail.component.html'
})
export class TransactionDetailComponent implements OnInit {
  private menuCollapsed: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  @ViewChild('lgModal') public lgModal:ModalDirective;

  public showChildModal():void {
    this.lgModal.show();
  }

  public hideChildModal():void {
    this.lgModal.hide();
  }

  onToggle() {
    this.menuCollapsed = !this.menuCollapsed;
  }

}
