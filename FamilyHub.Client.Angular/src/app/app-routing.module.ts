import { AuthGuardOld } from './core/guards/auth.guard.old';
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { MainLayoutComponent } from "./shared/layout/app-layouts/main-layout.component";
import { AuthLayoutComponent } from "./shared/layout/app-layouts/auth-layout.component";

const routes: Routes = [
  {
    path: "",
    component: MainLayoutComponent,
    data: { pageTitle: "Family Hub" },
    //canActivate: [AuthGuardOld],
    children: [
      {
        path: "",
        redirectTo: "home",
        pathMatch: "full"
      },
      {
        path: "home",
        loadChildren: "./features/home/home.module#HomeModule",
        data: { pageTitle: "Home" }
      },
      {
        path: "transactions",
        //canActivate: [AuthGuardOld],
        loadChildren: "./familyhub/transactions/transactions.module#TransactionsModule",
        data: { pageTitle: "Manage Transactions" }
      },

      // {
      //   path: "app-views",
      //   loadChildren: "./features/app-views/app-views.module#AppViewsModule",
      //   data: { pageTitle: "App Views" }
      // },

      // {
      //   path: "calendar",
      //   loadChildren:
      //     "app/features/calendar/calendar.module#CalendarFeatureModule"
      // },
      // {
      //   path: "dashboard",
      //   loadChildren: "./features/dashboard/dashboard.module#DashboardModule",
      //   data: { pageTitle: "Dashboard" }
      // },

      // {
      //   path: "e-commerce",
      //   loadChildren: "./features/e-commerce/e-commerce.module#ECommerceModule",
      //   data: { pageTitle: "E-commerce" }
      // },

      // {
      //   path: "forms",
      //   loadChildren:
      //     "./features/forms/forms-showcase.module#FormsShowcaseModule",
      //   data: { pageTitle: "Forms" }
      // },

      // {
      //   path: "graphs",
      //   loadChildren:
      //     "./features/graphs/graphs-showcase.module#GraphsShowcaseModule",
      //   data: { pageTitle: "Graphs" }
      // },

      // {
      //   path: "maps",
      //   loadChildren: "./features/maps/maps.module#MapsModule",
      //   data: { pageTitle: "Maps" }
      // },

      // {
      //   path: "miscellaneous",
      //   loadChildren:
      //     "./features/miscellaneous/miscellaneous.module#MiscellaneousModule",
      //   data: { pageTitle: "Miscellaneous" }
      // },
      // {
      //   path: "outlook",
      //   loadChildren: "./features/outlook/outlook.module#OutlookModule",
      //   data: { pageTitle: "Outlook" }
      // },
      // {
      //   path: "smartadmin",
      //   loadChildren:
      //     "./features/smartadmin-intel/smartadmin-intel.module#SmartadminIntelModule",
      //   data: { pageTitle: "Smartadmin" }
      // },

      // {
      //   path: "tables",
      //   loadChildren: "./features/tables/tables.module#TablesModule",
      //   data: { pageTitle: "Tables" }
      // },

      // {
      //   path: "ui",
      //   loadChildren:
      //     "./features/ui-elements/ui-elements.module#UiElementsModule",
      //   data: { pageTitle: "Ui" }
      // },

      // {
      //   path: "widgets",
      //   loadChildren:
      //     "./features/widgets/widgets-showcase.module#WidgetsShowcaseModule",
      //   data: { pageTitle: "Widgets" }
      // }
    ]
  },

  {
    path: "auth",
    component: AuthLayoutComponent,
    loadChildren: "./familyhub/auth/auth.module#AuthModule"
  },
  // { path: "**", redirectTo: "miscellaneous/error404" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
