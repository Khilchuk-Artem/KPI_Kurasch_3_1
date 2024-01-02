import {CanActivateFn, createUrlTreeFromSnapshot, Router} from '@angular/router';
import {inject} from "@angular/core";
import {CookieService} from "ngx-cookie-service";
import {AuthService} from "../services/auth-service";
import {jwtDecode} from "jwt-decode";

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService=inject(CookieService)
  const authService=inject(AuthService)
  const router=inject(Router)
  let token=cookieService.get('Authorization')
  if(token){
    token=token.replace("Bearer ", "")
  const decodedToken:any=jwtDecode(token)
    const authService=inject(AuthService)
    const expiration = decodedToken.exp*1000;
    const currentTime = new Date().getTime()
    if(expiration<currentTime){
      authService.logout()
      return router.createUrlTree(['/login'], {queryParams:{returnUrl: state.url}})
    }
    else {
      console.log("route data:",route.data['role'])
      if(route.data['role']){
        if((authService.getUser()?.roles.includes("Librarian")||route.data['role'].includes(authService.getUser()?.roles))){
          return true;
        }
        alert('Unauthorized')
        return false;
      }
      else {
        return true;
      }
    }
  }
  else {
    authService.logout()
    return router.createUrlTree(['/login'], {queryParams:{returnUrl: state.url}})
  }
};
