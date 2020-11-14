import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ApiUrl } from "app/models/ApiUrl";
import { Login } from "app/models/Login";
import { MenuUser } from "app/models/MenuUser";
import { Usuario } from "app/models/Usuario";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ServiceBaseService } from "./serviceBase.service";
const urlLogin = ApiUrl.baseUrl + "account/";
@Injectable({
  providedIn: "root",
})
export class LoginService extends ServiceBaseService {
  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(protected http: HttpClient, private router: Router) {
    super(http);
    this.baseUrl = urlLogin;
    this.currentUserSubject = new BehaviorSubject<any>(
      JSON.parse(localStorage.getItem("currentUser"))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }
  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem("currentUser");
    this.currentUserSubject.next(null);
    this.router.navigate(["/login"]);
  }
  public get currentUserValue(){
    return this.currentUserSubject.value;
  }
  login(login: Login) {
    console.log(login);
    return this.http.post<any>(`${urlLogin}login`, login).pipe(
      map((user) => {
        localStorage.setItem("currentUser", JSON.stringify(user));
        console.log(user);
        this.currentUserSubject.next(user);
        return user;
      })
    );
  }
}
