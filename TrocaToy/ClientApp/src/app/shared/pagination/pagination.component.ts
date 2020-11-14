import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
@Component({
  selector: "app-pagination",
  templateUrl: "./pagination.component.html",
  styleUrls: ["./pagination.component.css"],
})
export class PaginationComponent implements OnInit {
  currentPage = 1;
  backButtonDisabled = this.currentPage === 1;
  quantidadePaginas: number = 10;
  nextButtonDisabled =
    this.currentPage === this.quantidadePaginas || this.quantidadePaginas <= 4;
  pagesNext = 1;
  indexFirstItem: number = 1;
  indexLastItem: number = 3;
  maxQuantidade = 4;
  pages = null;

  selectPage(number) {}
  backPage() {
    this.pagesNext--;
    this.currentPage = this.pages[this.indexFirstItem] - this.maxQuantidade;
    this.pages = Array(this.maxQuantidade)
      .fill(this.currentPage)
      .map((x, i) => i + this.currentPage);
    this.backButtonDisabled = this.pages.includes(this.indexFirstItem);
    this.nextButtonDisabled = this.currentPage === this.quantidadePaginas;
  }

  nextPage() {
    this.pagesNext++;
    this.currentPage = this.pages[this.indexLastItem]++;
    console.log(this.currentPage);
    this.pages = [];
    for (
      let i = this.currentPage;
      i < this.currentPage + this.maxQuantidade;
      i++
    ) {
      if (i <= this.quantidadePaginas) {
        this.pages.push(i);
      }
    }
    this.nextButtonDisabled = this.pages.includes(this.quantidadePaginas);
    this.backButtonDisabled = this.currentPage === this.indexFirstItem;
  }
  constructor() {
    let numberpages =
      this.quantidadePaginas >= 5 ? this.maxQuantidade : this.quantidadePaginas;
    this.pages = Array(numberpages)
      .fill(1)
      .map((x, i) => i + 1);
  }

  ngOnInit() {}
}
