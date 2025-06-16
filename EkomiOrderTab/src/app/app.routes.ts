import { Routes } from '@angular/router';
import { OrdersComponent } from './Views/orders/orders.component';
import { LoginComponent } from './Views/login/login.component';
import { OrderDetailsComponent } from './Views/order-details/order-details.component';
import { Error404Component } from './Views/error404/error404.component';
import { routerGuard } from './guard/auth.guard';

export const routes: Routes = [     
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'orders', 
    component: OrdersComponent, canActivate: [routerGuard]
  },
    {
    path: 'order/details/:OrderId', 
    component: OrderDetailsComponent, canActivate: [routerGuard]
  },
  {
    path: '**',
    component: Error404Component
  }
];
