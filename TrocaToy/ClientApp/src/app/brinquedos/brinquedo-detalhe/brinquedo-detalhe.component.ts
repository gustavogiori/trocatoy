import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AnuncioService } from 'app/services/anuncio.service';
import { BrinquedoService } from 'app/services/brinquedo.service';
import { LoginService } from 'app/services/login.service';
let id = 0;
@Component({
  selector: 'app-brinquedo-detalhe',
  templateUrl: './brinquedo-detalhe.component.html',
  styleUrls: ['./brinquedo-detalhe.component.css']
})
export class BrinquedoDetalheComponent implements OnInit {
  titulo = "";
  anuncio;
  urlPrincipal = "";
  constructor(protected anuncioService: AnuncioService, public route: ActivatedRoute,
    protected router: Router, protected loginService: LoginService) { }
  isLogged() {
    return this.loginService.currentUserValue.user.nome == true;
  }
  mudouImagem(img) {
    this.urlPrincipal = img;
  }
  ngOnInit() {
    this.titulo = "Detalhes anuncio";
    this.route.params.subscribe((params: Params) => {
      id = params["id"];
      console.log(id);
      this.anuncioService.get(id).subscribe((data) => {
        this.anuncio = data;
        this.urlPrincipal = this.anuncio.urlPrincipal;
        console.log(data);
      });
    });
  }

}
