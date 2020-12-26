import { OnInit } from "@angular/core";
import { ServiceBaseService } from "app/services/serviceBase.service";
import { NotificationService } from "../messages/notification.service";

export class AddBase implements OnInit {
  hasError: boolean;
  msgsErro: string[];
  constructor(protected service: ServiceBaseService,
    protected notificationService: NotificationService) {

  }
  afterSave(response) {

  }
  salvar(obj: any) {
    this.hasError = false;
    this.msgsErro = [];
    this.service.create(obj).subscribe(
      (response) => {
        this.notificationService.notify("Cadastro realizado com sucesso!");
        this.hasError = false;
        this.afterSave(response);
      },
      (erro) => {
        this.hasError = true;
        console.log(erro);
        if (!erro.ok) {
          this.msgsErro = erro.error.errorMessage;
        }
      }
    );
  }
  ngOnInit(): void {
  }
}
