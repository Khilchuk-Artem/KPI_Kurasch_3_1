import {HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest} from '@angular/common/http';
import {Injectable} from "@angular/core";
import {CookieService} from "ngx-cookie-service";
import {Observable} from "rxjs";
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private cookieService: CookieService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    //if (this.shouldInterceptRequest(request)) {
      //console.log(this.cookieService.get('Authorization'))
    var bearer = this.cookieService.get('Authorization')
      const authRequest = request.clone({
        setHeaders: {
          'Authorization': bearer
        }
      });

      return next.handle(authRequest);
    //}
    return next.handle(request);
  }

  /*private shouldInterceptRequest(request: HttpRequest<any>): boolean {
    return request.urlWithParams.indexOf('addAuth=true', 0) > -1? true: false;
  }*/
}
