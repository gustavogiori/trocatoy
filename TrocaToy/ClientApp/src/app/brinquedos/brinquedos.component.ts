import { Component, OnInit } from "@angular/core";
import {
  trigger,
  state,
  style,
  transition,
  animate,
} from "@angular/animations";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";

import "rxjs/add/operator/switchMap";
import "rxjs/add/operator/do";
import "rxjs/add/operator/debounceTime";
import "rxjs/add/operator/distinctUntilChanged";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/from";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";
import { PagerService } from "../services/pager.service";
import { AnuncioService } from "app/services/anuncio.service";
import { Anuncio } from "app/models/Anuncio";
import { LoginService } from "app/services/login.service";

@Component({
  selector: "app-brinquedos",
  templateUrl: "./brinquedos.component.html",
  styleUrls: ["./brinquedos.component.css"],
  animations: [
    trigger("toggleSearch", [
      state(
        "hidden",
        style({
          opacity: 0,
          "max-height": "0px",
        })
      ),
      state(
        "visible",
        style({
          opacity: 1,
          "max-height": "70px",
          "margin-top": "20px",
        })
      ),
      transition("* => *", animate("250ms 0s ease-in-out")),
    ]),
  ],
})
export class BrinquedosComponent implements OnInit {
  searchBarState = "hidden";
  restaurants: any[];
  pag: Number = 1;
  contador: Number = 3;
  totalItens: number = 0;
  searchForm: FormGroup;
  searchControl: FormControl;
  nomes = Array<Anuncio>();
  private allItems = Array<Anuncio>();
  pager: any = {};
  pagedItems: any[];

  setPage(page: number) {
    this.pager = this.pagerService.getPager(this.totalItens, page);
    this.anuncioServer.getAll(page, 5).subscribe((data) => {
      this.nomes = data.data;
    });
  }
  constructor(private fb: FormBuilder, private router: Router,
    private pagerService: PagerService,
    private anuncioServer: AnuncioService,
    protected loginService: LoginService) { }

  isLogged() {
    return this.loginService.currentUserValue.user.nome == true;
  }
  ngOnInit() {
    this.anuncioServer.getAll(1, 5).subscribe((data) => {
      this.allItems = data.data;
      this.totalItens = data.totalRecords;
      this.setPage(1);
    });
    this.searchControl = this.fb.control("");
    this.searchForm = this.fb.group({
      searchControl: this.searchControl,
    });
  }
  clickDetalhe(id) {
    this.router.navigateByUrl("/brinquedos/detalhes/" + id);
  }

  fazerProposta(id) {
    this.router.navigateByUrl("/brinquedos/addProposta/" + id);
  }
  toggleSearch() {
    this.searchBarState =
      this.searchBarState === "hidden" ? "visible" : "hidden";
  }
}
