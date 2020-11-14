import { HttpClient } from "@angular/common/http";
import {
  Component,
  Input,
  OnInit,
  SimpleChanges,
} from "@angular/core";
import { Router } from "@angular/router";
import { ServiceBaseService } from "app/services/serviceBase.service";
import { NotificationService } from "../messages/notification.service";

@Component({
  selector: "app-list-base",
  templateUrl: "./list-base.component.html",
  styleUrls: ["./list-base.component.css"],
})
export class ListBaseComponent implements OnInit {
  @Input() labelNewRegister = "Novo";
  @Input() title = "";
  @Input() tableHeads: Array<String> = new Array<String>();
  @Input() tableHeadsCode: Array<String> = new Array<String>();
  @Input() tableDatas = [];
  @Input() tableColName: Array<String> = new Array<String>();
  tableColNameGenerated: Array<String> = new Array<String>();
  isTableColNameSet: Boolean = false;
  @Input() columnKey;
  @Input() text;
  @Input() urlRedirectToCreate = "";
  @Input() urlRedirectToEdit = "";
  @Input() urlRedirectToDelete = "";


  server: ServiceBaseService;

  campo = "";
  valor = "";
  redirectToCreate() {
    this.router.navigateByUrl(this.urlRedirectToCreate);
  }
  redirectToEdit(id) {
    this.router.navigateByUrl(this.urlRedirectToEdit + id);
  }
  redirectToDelete(id) {
    this.router.navigateByUrl(this.urlRedirectToDelete + id);
  }

  private getKeys(value: any): Array<String> {
    let keys = new Array<String>();
    let teste = Object.keys(value);
    teste.forEach((element) => {
      console.log(this.tableHeadsCode);
      if (this.tableHeadsCode.indexOf(element) !== -1) {
        keys.push(element);
      } else {
      }
    });
    console.log(keys);
    return keys;
  }
  private isHeadAndColLengthSame(
    head: Array<String>,
    col: Array<String>
  ): Boolean {
    console.log("head.length" + head.length);
    console.log("col.length" + col.length);
    return head.length === col.length;
  }

  constructor(
    public router: Router,
    protected http: HttpClient,
    protected notificationService: NotificationService,
    protected serviceBase: ServiceBaseService
  ) {}
  data: any;

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["tableHeads"]) {
      if (this.tableHeads.length > 0) {
        // console.log('tableHeads');
      }
    }

    if (changes["tableDatas"]) {
      console.log(this.tableDatas);
      if (!this.isTableColNameSet) {
        if (this.tableDatas !== undefined) {
          if (this.tableDatas.length > 0) {
            this.tableColNameGenerated = this.getKeys(this.tableDatas[0]);
            if (
              !this.isHeadAndColLengthSame(
                this.tableHeads,
                this.tableColNameGenerated
              )
            ) {
              console.error(
                "Table column row is not same as with property name in self generated"
              );
            }
          }
        }
      }
    }
  }
}
