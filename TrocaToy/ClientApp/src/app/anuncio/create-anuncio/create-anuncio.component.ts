import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Anuncio } from "app/models/Anuncio";
import { AnuncioService } from "app/services/anuncio.service";
import { BrinquedoService } from "app/services/brinquedo.service";
import { EnderecoService } from "app/services/endereco.service";
import { AddBase } from "app/shared/CRUD/AddBase";
import { NotificationService } from "app/shared/messages/notification.service";

@Component({
  selector: "app-create-anuncio",
  templateUrl: "./create-anuncio.component.html",
  styleUrls: ["./create-anuncio.component.css"],
})
export class CreateAnuncioComponent extends AddBase {
  constructor(
    protected anuncioService: AnuncioService,
    protected notificationService: NotificationService,
    private formBuilder: FormBuilder,
    protected brinquedoService: BrinquedoService,
    protected enderecoService: EnderecoService
  ) {
    super(anuncioService, notificationService);
  }
  brinquedos: Array<any>;
  enderecos: Array<any>;
  tiposDisponibilidade: Array<any>;
  anuncioForm: FormGroup;
  titulo = "";
  faIcon = "fa-gamepad";
  ngOnInit() {
    this.initializeForm();
    this.fillBrinquedos();
    this.fillEnderecos();
    this.fillTipoDisponibilidade();
    this.titulo = "Cadastro de anuncio";
    this.faIcon = "fa-gamepad";
  }
  fillTipoDisponibilidade() {
    this.tiposDisponibilidade = new Array<any>();
    this.tiposDisponibilidade.push({ codigo: "1", descricao: "Troca" });
    this.tiposDisponibilidade.push({ codigo: "2", descricao: "Venda" });
    this.tiposDisponibilidade.push({ codigo: "3", descricao: "Ambos" });
  }
  fillBrinquedos() {
    this.brinquedoService.getAll(1, 100).subscribe(
      (req) => {
        this.brinquedos = req.data;
        console.log(req.data);
      },
      (error) => {
        this.notificationService.notify(error.messsage);
      }
    );
  }
  fillEnderecos() {
    this.enderecoService.getAllNotPagination().subscribe(
      (req) => {
        this.enderecos = req;
        console.log(req);
      },
      (error) => {
        this.notificationService.notify(error.messsage);
      }
    );
  }
  salvarAnuncio(anuncio: Anuncio) {
    console.log(anuncio);
    this.salvar(anuncio);
  }
  initializeForm() {
    this.anuncioForm = this.formBuilder.group({
      IdBrinquedo: this.formBuilder.control("", [Validators.required]),
      IdEnderecoEntrega: this.formBuilder.control("", [
        Validators.compose([Validators.required]),
      ]),
      TipoDisponibilidade: this.formBuilder.control("", [
        Validators.compose([Validators.required]),
      ]),
      TelefoneContato: this.formBuilder.control("", [
        Validators.compose([Validators.required]),
      ]),
    });
  }
}
