import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(
    private RouterService: Router,
    private Auth: AuthService
  ){}

  GoToOrders(){
    this.RouterService.navigate(['/orders']);
  }

  Login(){
    const _userId = "1234";
      this.Auth.GenerateAuthToken(_userId).then((res:any)=>{
        this.Auth.setToken(res.token);
        if(this.Auth.getToken()){
          this.GoToOrders();
        }
    })
  }
  
}
