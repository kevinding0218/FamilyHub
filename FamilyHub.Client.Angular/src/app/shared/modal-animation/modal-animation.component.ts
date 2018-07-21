import { Component, OnInit, Input, ViewEncapsulation, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-modal-animation',
  templateUrl: './modal-animation.component.html',
  styleUrls: ['./modal-animation.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ModalAnimationComponent implements OnInit, OnDestroy {

  @Input() modalClass: string;
  @Input() contentClass: string;
  @Input() modalID: string;
  @Input() backDrop = false;
  @Input() containerClick = true;

  private modalAnimationSub: Subscription;

  constructor(private modalService: SharedService) { }

  ngOnInit() {
    this.modalAnimationSub = this.modalService.modalAnimationAction$.subscribe((ele) => {
      if (ele.action === 'open') {
        this.openModal(ele.modalID);
      } else if (ele.action === 'close') {
        this.closeModal(ele.modalID);
      }
    });
  }

  private openModal(modalID: string): void {
    document.querySelector('#' + modalID).classList.add('md-show');
  }

  private closeModal(modalID: string) {
    document.querySelector('#' + modalID).classList.remove('md-show');
  }

  private onContainerClicked(modalID: string): void {
    if (this.containerClick === true) {
      document.querySelector('#' + modalID).classList.remove('md-show');
    }
  }

  ngOnDestroy() {
    this.modalAnimationSub.unsubscribe();
  }
}
