import { Component, OnInit } from "@angular/core";
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { LoginComponent } from "app/login/login.component";
import { Cidade } from "app/models/Cidade";
import { Endereco } from "app/models/Endereco";
import { Estado } from "app/models/Estado";
import { Login } from "app/models/Login";
import { Usuario } from "app/models/Usuario";
import { CEPService } from "app/services/CEP.service";
import { CidadeService } from "app/services/cidade.service";
import { EstadoService } from "app/services/estado.service";
import { UsuarioService } from "app/services/usuario.service";
import { Constantes } from "app/shared/constantes";
import { AddBase } from "app/shared/CRUD/AddBase";
import { Validations } from "app/shared/Validations";
import { NotificationService } from "../../shared/messages/notification.service";

@Component({
  selector: "app-create-usuario",
  templateUrl: "./create-usuario.component.html",
})
export class CreateUsuarioComponent extends AddBase {
  userForm: FormGroup;
  addressForm: FormGroup;
  estados: Estado[];
  cidades: Cidade[];
  todasCidades: Cidade[];
  constructor(
    protected formBuilder: FormBuilder,
    protected notificationService: NotificationService,
    protected userService: UsuarioService,
    protected estadoService: EstadoService,
    protected cidadeService: CidadeService,
    protected cepService: CEPService,
    protected router: Router
  ) {
    super(userService, notificationService);
  }

  salvarUsuario(userForm: Usuario, addressForm: Endereco) {
    console.log(addressForm);
    userForm.Endereco = [addressForm];
    this.salvar(userForm);
  }
  afterSave() {
    this.router.navigateByUrl("/login");
  }
  carregaEstados() {
    this.estadoService.getAll(1, 100).subscribe(
      (response) => {
        this.estados = response.data;
        console.log(response);
      },
      (error) => {
        console.log(error);
        this.notificationService.notify(error.message);
      }
    );
  }
  setaEnderecoFromCep(response) {
    console.log(response);
    if (response.erro == true) {
      this.limpaDadosEndereco();
      return;
    }
    this.addressForm.get("rua").setValue(response.logradouro);
    var estadoFromCep = this.estados.find(function (estado) {
      return estado.codigo === response.uf;
    });
    this.addressForm.get("IdEstado").setValue(estadoFromCep.id);
    this.mudouEstado();
    var cidadeFromCep = this.todasCidades.find(function (cidade) {
      return (
        cidade.descricao.toUpperCase() === response.localidade.toUpperCase()
      );
    });

    this.addressForm.get("IdCidade").setValue(cidadeFromCep.id);
  }
  digitouCep(cep) {
    this.cepService.GetCep(cep).subscribe(
      (response) => {
        console.log(response);
        this.setaEnderecoFromCep(response);
      },
      (error) => {
        this.limpaDadosEndereco();
      }
    );
  }
  limpaDadosEndereco() {
    this.addressForm.get("IdEstado").setValue("");
    this.mudouEstado();
    this.addressForm.get("IdCidade").setValue("");
    this.addressForm.get("rua").setValue("");
  }
  mudouEstado() {
    var idEstado = this.addressForm.get("IdEstado").value;
    this.cidades = this.todasCidades.filter(function (city) {
      return city.idEstado === idEstado;
    });
  }
  carregaCidades() {
    this.cidadeService.getAll(1, 1000).subscribe(
      (response) => {
        console.log(response);
        this.todasCidades = response.data;
      },
      (error) => {
        console.log(error);
        this.notificationService.notify(error.message);
      }
    );
  }
  initializeUserForm() {
    this.userForm = this.formBuilder.group({
      nome: this.formBuilder.control("", [
        Validators.required,
        Validators.minLength(5),
      ]),
      email: this.formBuilder.control("", [
        Validators.compose([
          Validators.required,
          Validators.pattern(Constantes.emailPattern),
        ]),
      ]),
      cpf: this.formBuilder.control("", [
        Validators.compose([Validators.required, Validations.ValidaCpf]),
      ]),
      telefone: this.formBuilder.control("", [
        Validators.compose([Validators.required]),
      ]),
      senha: this.formBuilder.control(
        "",
        Validators.compose([Validators.required])
      ),
    });
  }
  initializeAddressForm() {
    this.addressForm = this.formBuilder.group({
      rua: this.formBuilder.control("", [
        Validators.required,
        Validators.minLength(5),
      ]),
      numero: this.formBuilder.control("", [
        Validators.required,
        Validators.pattern(Constantes.numberPattern),
      ]),
      cep: this.formBuilder.control("", [Validators.required]),
      complemento: this.formBuilder.control(""),
      IdEstado: this.formBuilder.control("", [Validators.required]),
      IdCidade: this.formBuilder.control("", Validators.required),
    });
  }
  ngOnInit() {
    this.carregaEstados();
    this.carregaCidades();
    this.initializeUserForm();
    this.initializeAddressForm();
  }
}
