import { Component, OnInit } from "@angular/core";
import { LoginService } from "app/services/login.service";

@Component({
  selector: "app-login-nav",
  templateUrl: "./login-nav.component.html",
  styleUrls: ["./login-nav.component.css"],
})
export class LoginNavComponent implements OnInit {
  constructor(private loginService: LoginService) {}

  logout(){
    this.loginService.logout();
  }
  currentUser() {
    console.log("current");
    console.log(this.loginService.currentUserValue);
    return this.loginService.currentUserValue;
  }
  ngOnInit() {}
}
