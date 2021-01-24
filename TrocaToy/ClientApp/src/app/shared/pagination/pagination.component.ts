import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
@Component({
  selector: "app-pagination",
  templateUrl: "./pagination.component.html",
  styleUrls: ["./pagination.component.css"],
})
export class PaginationComponent implements OnInit {
  restaurants: any[];
  pag: Number = 1;
  contador: Number = 3;
  totalItens: number = 0;
  // pager object
  pager: any = {};

  // paged items
  pagedItems: any[];
  ngOnInit() { }
  setPage(page: number) {
  }
}
