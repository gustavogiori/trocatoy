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
  pag : Number = 1 ;
  contador : Number = 3;
  searchForm: FormGroup;
  searchControl: FormControl;
  nomes=[{nome:"teste"},
  {nome:"teste"},
  {nome:"teste"},
  {nome:"teste"},
  {nome:"teste"}];

  constructor(private fb: FormBuilder, private router: Router) {}
mudouPagina(e){
  alert(e);
}
  ngOnInit() {
    this.searchControl = this.fb.control("");
    this.searchForm = this.fb.group({
      searchControl: this.searchControl,
    });
  }
  clickDetalhe() {
    this.router.navigateByUrl("/brinquedos/detalhes/1");
  }
  toggleSearch() {
    this.searchBarState =
      this.searchBarState === "hidden" ? "visible" : "hidden";
  }
}
