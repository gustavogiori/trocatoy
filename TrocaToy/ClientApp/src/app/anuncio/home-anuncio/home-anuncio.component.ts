import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AnuncioService } from "app/services/anuncio.service";
import { LoginService } from "app/services/login.service";
import { ListBase } from "app/shared/CRUD/ListBase";


@Component({
  selector: "app-home-anuncio",
  templateUrl: "./home-anuncio.component.html",
  styleUrls: ["./home-anuncio.component.css"],
})
export class HomeAnuncioComponent extends ListBase {

  constructor(
    public router: Router,
    protected anuncioService: AnuncioService,
    protected loginService: LoginService
  ) {
    super(router, anuncioService);
    console.log(loginService);
    console.log(anuncioService);
    this.text = "Estado";
    this.columnKey = "Id";
    this.redirectToCreate="AddAnuncio";
    this.tableHead = new Array<String>("Disponibilidade", "Telefone","Cod. Brinquedo");
    this.tableHeadCode = new Array<String>("TipoDisponibilidade", "TelefoneContato","IdBrinquedo");
  }


  ngOnInit() {
    super.ngOnInit();
  }
}
