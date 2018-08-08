import { Injectable } from '@angular/core';

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
    label: 'Navigation',
    main: [
      {
        state: 'dashboard',
        short_label: 'D',
        name: 'Dashboard',
        type: 'link',
        icon: 'feather icon-home'
      },
      {
        state: 'member',
        short_label: 'M',
        name: 'Member',
        type: 'sub',
        icon: 'feather icon-users',
        children: [
          {
            state: 'member-list',
            name: 'Member List Simple'
          },
          {
            state: 'member-ngrx-store',
            name: 'Member List Advance'
          }
        ]
      },
      {
        state: 'datatabledemos',
        short_label: 'DT',
        name: 'Datatable Demos',
        type: 'sub',
        icon: 'feather icon-inbox',
        children: [
          {
            state: 'ag-grid',
            name: 'AG Grid'
          },
          {
            state: 'ng2-smart-table',
            name: 'Ng2 Smart Table'
          },
          {
            state: 'jq-datatable',
            name: 'Jq Datatable'
          },
          {
            state: 'prime-ng-table',
            name: 'Prime Ng Table'
          },
          {
            state: 'ejs-datatable',
            name: 'EJS Data Table'
          }
        ]
      },
      {
        state: 'finance',
        short_label: 'F',
        name: 'Finance',
        type: 'sub',
        icon: 'feather icon-inbox',
        children: [
          {
            state: 'analytics',
            name: 'Analytics',
            badge: [
              {
                type: 'info',
                value: 'NEW'
              }
            ]
          },
          {
            state: 'current-month',
            name: 'Current Month'
          },
          {
            state: 'transaction-history',
            name: 'Transaction History'
          },
          {
            state: 'manual-import',
            name: 'Manual Import'
          },
          {
            state: 'settings',
            name: 'Settings'
          }
        ]
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
