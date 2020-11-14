import { EventEmitter, OnInit, Output } from "@angular/core";
import { Router } from "@angular/router";
import { ServiceBaseService } from "app/services/serviceBase.service";

export class ListBase implements OnInit {
  constructor(public router: Router, protected service: ServiceBaseService) {
    this.loadItens(1);
  }
  text = "";
  redirectToCreate = "";
  redirectToEdit = "";
  redirectToDelete = "";
  columnKey = "";
  baseUrl;
  loading = false;
  records: [];
  tableHead: Array<String>;
  tableHeadCode: Array<String>;
  tableColName: Array<String>;

  protected loadItens(pageIndex) {

    this.service.getAll(pageIndex, 100).subscribe(
      (record) => {
        this.records = record.data;

        this.loading = false;
      },
      (error) => {
        if (error.status === 401) {
          this.router.navigateByUrl("dashboard/login");
        }
        this.records = [];
        console.log(`Erro ao tentar Carregar eventos: ${error}`);
        console.log(error);
        this.loading = false;
      }
    );
  }
  ngOnInit(): void {
  }
}
