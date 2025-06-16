import { HttpInterceptorFn } from "@angular/common/http";
import { AuthService } from "../Services/auth.service";

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {

    const token = AuthService.getCookie('Authtoken');

    if(token){
        req = req.clone({
            setHeaders: { Authorization: `Bearer ${token}` },
        });
    }
    return next(req);
  };
  