import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../../Services/orders.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoadingTimeout } from '../../../environments/environment';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent implements OnInit {

  OrderDetails:any;
  Order:any;
  isLoading: boolean = true;
  OrderSummary:any;

  constructor(
    private OrdersService: OrdersService,
    private route: ActivatedRoute,
    private router: Router
  ){}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const OrderId = params.get('OrderId')!;
      this.GetOrderDetails(OrderId);
      this.GetOrder(OrderId);
      this.GetOrderSummary(OrderId);
    });      
  }

  GetOrderDetails(OrderId:string){
    this.OrdersService.GetOrderDetails(OrderId).then((res:any)=>{
      console.log(res)
        if(res){
          if(res.length > 0){
              this.OrderDetails = res;
          } else {
            this.router.navigate(["**"])
          }
        }
    });
  }

  GetOrder(OrderId:any){
    this.OrdersService.GetOrder(OrderId).then((res:any)=>{
      this.Order = res;
      if(res){
        setTimeout(() => {
            this.isLoading = false;
        }, LoadingTimeout.timeout);
      }    
    });
  }

  GetOrderSummary(OrderId:any){
    this.OrdersService.GetOrderSummary(OrderId).then((res:any)=>{
      this.OrderSummary = res;
    });
  }

  GoToOrders(){
    this.router.navigate(["orders"])
  }

}
