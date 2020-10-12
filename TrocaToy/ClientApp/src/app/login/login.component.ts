import { renderFlagCheckIfStmt } from "@angular/compiler/src/render3/view/template";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Login } from "app/models/Login";
import { LoginService } from "app/services/login.service";
import { Constantes } from "app/shared/constantes";
import { AddBase } from "app/shared/CRUD/AddBase";
import { NotificationService } from "app/shared/messages/notification.service";
import { Validations } from "app/shared/Validations";
import { first } from "rxjs/operators";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent extends AddBase {
  loginForm: FormGroup;

  constructor(
    protected loginService: LoginService,
    protected notificationService: NotificationService,
    protected formBuilder: FormBuilder,
    private router: Router
  ) {
    super(loginService, notificationService);
  }
  logar(dadosLogin: Login) {
    this.loginService
      .login(dadosLogin)
      .pipe(first())
      .subscribe(
        (data) => {
          this.router.navigate(["/"]);
        },
        (error) => {
          this.hasError=true;
          console.log(error.error);
          this.msgsErro=error.error.errors;
        }
      );
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Email: this.formBuilder.control(
        "",
        Validators.compose([
          Validators.required,
          Validators.pattern(Constantes.emailPattern),
        ])
      ),
      Senha: this.formBuilder.control(
        "",
        Validators.compose([Validators.required])
      ),
    });
  }
}
