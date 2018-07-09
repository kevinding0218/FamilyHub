
import { RouterModule, Routes } from "@angular/router";


export const routes: Routes = [
    {
        path: 'analytics',
        loadChildren: './analytics/analytics.module#AnalyticsModule'
    },
    {
        path: 'trans-detail',
        loadChildren: './transaction-detail/transaction-detail.module#TransactionDetailModule'
    },
    {
        path: 'payment-options',
        loadChildren: './payment/payment.module#PaymentModule'
    }
];

export const routing = RouterModule.forChild(routes);
