import {Injectable} from '@angular/core';

export interface BadgeItem {
  type: string;
  value: string;
}

export interface ChildrenItems {
  state: string;
  target?: boolean;
  name: string;
  type?: string;
  children?: ChildrenItems[];
}

export interface MainMenuItems {
  state: string;
  short_label?: string;
  main_state?: string;
  target?: boolean;
  name: string;
  type: string;
  icon: string;
  badge?: BadgeItem[];
  children?: ChildrenItems[];
}

export interface Menu {
  label: string;
  main: MainMenuItems[];
}

const MENUITEMS = [
  {
    label: 'My Panel',
    main: [
      {
        state: 'simple-page',
        short_label: 'S',
        name: 'Sample Page',
        type: 'link',
        icon: 'feather icon-file'
      },
      {
        state: 'coming-soon',
        short_label: 'CS',
        name: 'Coming Soon',
        type: 'link',
        icon: 'feather icon-watch',
        target: true
      }
    ]
  }
];


@Injectable()
export class MenuItems {
  getAll(): Menu[] {
    return MENUITEMS;
  }
}
