import { Component, OnInit } from '@angular/core';
import { OrdersService } from './Services/orders.service';
import { AuthService } from './Services/auth.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {

  title = 'EkomiOrderTab';
  Orders:any[] = [];

  constructor(
    private AuthService: AuthService,
    private router: Router
  ){
    if(!this.AuthService.isLoggedInAndNotExpired()){
      this.AuthService.clearToken();
      this.router.navigate([''])
    } else {
      this.router.navigate(['orders'])
    }
  }



}
