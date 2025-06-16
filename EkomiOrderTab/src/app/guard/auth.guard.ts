import { inject } from "@angular/core";
import { CanActivateFn } from "@angular/router";
import { AuthService } from "../Services/auth.service";
import { Router } from "@angular/router";

export const routerGuard: CanActivateFn =  (route, state) => {

    const authService = inject(AuthService);
    const router = inject(Router);

    if(authService.isLoggedInAndNotExpired()){
        return true;
    } else {
        authService.clearToken();
        router.navigate([""])
        return false;        
    }
}