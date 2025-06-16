import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment as env } from '../../environments/environment';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private api: HttpClient
  ) { }

  GenerateAuthToken(UserId:string){
    return firstValueFrom(this.api.get<any>(env.API_BASE_URL + `/auth/${UserId}`));
  }

  setToken(token: string): void {
    document.cookie = `Authtoken=${token}; path=/`;
  }  

  static getCookie(name: string): string | null {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop()?.split(';').shift() || null;
    return null;
  }

  isLoggedInAndNotExpired(): boolean {
    const token = this.getToken();
    return !!token && !this.isTokenExpired(token);
  }

  getToken(): string | null {
    const cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Authtoken\s*=\s*([^;]*).*$)|^.*$/, '$1');
    return cookieValue || null;
  }

  isTokenExpired(token: string): boolean {
    try {
      const decodedToken = jwtDecode(token);
      const now = Date.now() / 1000;
      return decodedToken.exp ? decodedToken.exp < now : true;
    } catch (error) {
      console.error('Error decoding token:', error);
      return true;
    }
  }

   clearToken(): void {
    document.cookie = 'token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
  }

}
