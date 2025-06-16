import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment as env } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(
    private api: HttpClient
  ) { }
  
  GetOrders(){
    return firstValueFrom(this.api.get<any>(env.API_BASE_URL + `/order/get/all`));
  }

  GetOrderDetails(OrderId: string){
    return firstValueFrom(this.api.get<any>(env.API_BASE_URL + `/order/get/details/${OrderId}`));
  }

  GetOrder(OrderId: string){
    return firstValueFrom(this.api.get<any>(env.API_BASE_URL + `/order/get/${OrderId}`));
  }

  GetOrderSummary(OrderId: string){
    return firstValueFrom(this.api.get<any>(env.API_BASE_URL + `/order/get/summary/${OrderId}`));
  }

  UpdateOrderStatus(OrderId:string, OrderStatusId:any){
    return firstValueFrom(this.api.post<any>(env.API_BASE_URL + `/order/update/status`, {OrderId:OrderId, OrderStatusId: OrderStatusId}));
  }

}
