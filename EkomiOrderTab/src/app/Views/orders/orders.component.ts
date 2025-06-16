import { Component, OnDestroy, OnInit } from '@angular/core';
import { OrdersService } from '../../Services/orders.service';
import { AuthService } from '../../Services/auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { LoadingTimeout } from '../../../environments/environment';
import { FormsModule } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { EventServiceService } from '../../Services/event-service.service';
import {NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, FormsModule, NgbPaginationModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})

export class OrdersComponent implements OnInit, OnDestroy {

  Orders: any[] = [];
  ProductImages: any[] = [];
  isLoading:boolean = true;
  TotalOrderAmount:any;
  TotalOrderItem:any;
  selectedStatus: number = 0;
  AllOrders: any[] = [];
  private userDataUpdateSubscription!: Subscription;
  total$!: Observable<number>;
  page: number = 1;
	pageSize: number = 5;
  filteredOrders: any[] = [];


  constructor(
    private OrdersService: OrdersService,
    private AuthService: AuthService,
    private router: Router,
    private DataUpdate: EventServiceService
  ){}

  ngOnInit(): void {
    this.InitializeAuthToken("1234");
    this.GetOrders();
    this.userDataUpdateSubscription = this.DataUpdate.userDataUpdated$.subscribe(() => {
      this.isLoading = true;
      this.GetOrders();
    });
  }

  ngOnDestroy(): void {
    this.userDataUpdateSubscription.unsubscribe();
  }

  InitializeAuthToken(UserId: string){
    this.AuthService.GenerateAuthToken(UserId).then((res: any) => {
      if(res && res.token) {
        this.AuthService.setToken(res.token);
        return res;
      }
    });
    return null;
  }

  GetOrders(){
    this.OrdersService.GetOrders().then((res:any)=>{
      this.AllOrders = res;
      console.log(this.AllOrders, "allorders")
      this.filterOrders();
      res.forEach((_order:any) => {
        this.GetOrderDetails(_order.OrderId);
      });
    setTimeout(() => {
          this.isLoading = false;
    }, LoadingTimeout.timeout);
    });
  }

  GetOrderDetails(OrderId: string){
    this.OrdersService.GetOrderDetails(OrderId).then((res:any)=>{
      this.ProductImages = res;
    });
  }

  GoToOrderDetails(OrderId: string){
    this.router.navigate(['/order/details', OrderId]);
  }

filterOrders() {
  if (this.selectedStatus == 1) {
    this.filteredOrders = this.AllOrders.filter(o => o.OrderStatus == 1);
  } else if (this.selectedStatus == 2) {
    this.filteredOrders = this.AllOrders.filter(o => o.OrderStatus == 2);
  } else if (this.selectedStatus == 3) {
    this.filteredOrders = this.AllOrders.filter(o => o.OrderStatus == 3);
  } else if (this.selectedStatus == 5) {
    this.filteredOrders = this.AllOrders.filter(o => o.OrderStatus == 5);
  } else {
    this.filteredOrders = [...this.AllOrders];
  }
  this.page = 1;
  this.TotalOrderAmount = this.filteredOrders.reduce((sum, order) => sum + (order.OrderTotal || 0), 0);
  this.TotalOrderItem = this.filteredOrders.reduce((sum, order) => sum + (order.TotalOrderItems || 0), 0);
}

MarkOrderAsPaid(OrderId:string, OrderStatusId:any){
  this.OrdersService.UpdateOrderStatus(OrderId, OrderStatusId).then((res:any)=>{
    if(res> 0){
      this.DataUpdate.triggerUserDataUpdate();
    } else {
      console.log("something's not right")
    }
  });  
}  

get paginatedOrders() {
  const start = (this.page - 1) * this.pageSize;
  const end = start + this.pageSize;
  return this.filteredOrders.slice(start, end);
}

}
