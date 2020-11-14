import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BrinquedoService } from "app/services/brinquedo.service";
import { AddBase } from "app/shared/CRUD/AddBase";
import { NotificationService } from "app/shared/messages/notification.service";

@Component({
  selector: "app-add-brinquedo",
  templateUrl: "./add-brinquedo.component.html",
  styleUrls: ["./add-brinquedo.component.css"],
})
export class AddBrinquedoComponent extends AddBase {
  brinquedoForm: FormGroup;

  constructor(
    protected brinquedoService: BrinquedoService,
    protected notificationService: NotificationService,
    protected formBuilder: FormBuilder
  ) {
    super(brinquedoService, notificationService);
    this.titulo="Cadastro de brinquedos";
  }
  titulo = "";
  initializeForm() {
    this.brinquedoForm = this.formBuilder.group({
      nome: this.formBuilder.control("", [
        Validators.required,
        Validators.minLength(5),
      ]),
      marca: this.formBuilder.control("", [
        Validators.compose([Validators.required]),
      ]),
      novo: this.formBuilder.control(""),
    });
  }
  ngOnInit() {
    this.initializeForm();
  }
}
