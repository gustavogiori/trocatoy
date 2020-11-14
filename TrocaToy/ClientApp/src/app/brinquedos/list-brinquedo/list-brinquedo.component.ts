import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { BrinquedoService } from "app/services/brinquedo.service";
import { ListBase } from "app/shared/CRUD/ListBase";

@Component({
  selector: "app-list-brinquedo",
  templateUrl: "./list-brinquedo.component.html",
  styleUrls: ["./list-brinquedo.component.css"],
})
export class ListBrinquedoComponent extends ListBase {
  constructor(
    public router: Router,
    protected brinquedoService: BrinquedoService
  ) {
    super(router, brinquedoService);
    console.log(brinquedoService);
    this.text = "Categoria";
    this.redirectToEdit = "dashboard/editCategoria/";
    this.redirectToDelete = "dashboard/deleteCategoria/";
    this.columnKey = "id";
    this.tableHead = new Array<String>("Descrição", "Marca", "Novo?");
    this.tableHeadCode = new Array<String>("nome", "marca", "novo");
    this.redirectToCreate = "AddBrinquedo";
  }
  ngOnInit(){
    super.ngOnInit();
  }
}
