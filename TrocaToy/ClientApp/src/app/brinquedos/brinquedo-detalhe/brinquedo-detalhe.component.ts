import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-brinquedo-detalhe',
  templateUrl: './brinquedo-detalhe.component.html',
  styleUrls: ['./brinquedo-detalhe.component.css']
})
export class BrinquedoDetalheComponent implements OnInit {
titulo="";
  constructor() { }

  ngOnInit() {
    this.titulo="Detalhes anuncio";
  }

}
