import { Routes } from "@angular/router";
import { CreateAnuncioComponent } from "./anuncio/create-anuncio/create-anuncio.component";
import { HomeAnuncioComponent } from "./anuncio/home-anuncio/home-anuncio.component";
import { AddBrinquedoComponent } from "./brinquedos/add-brinquedo/add-brinquedo.component";
import { BrinquedoDetalheComponent } from "./brinquedos/brinquedo-detalhe/brinquedo-detalhe.component";

import { BrinquedosComponent } from "./brinquedos/brinquedos.component";
import { ListBrinquedoComponent } from "./brinquedos/list-brinquedo/list-brinquedo.component";
import { LoginComponent } from "./login/login.component";
import { AddPropostaComponent } from "./proposta/add-proposta/add-proposta.component";
import { CreateUsuarioComponent } from "./usuario/create-usuario/create-usuario.component";
import { AuthGuard } from "./_helper/auth.guard";

export const ROUTES: Routes = [
  { path: "", component: BrinquedosComponent },
  { path: "brinquedos", component: BrinquedosComponent },
  { path: "brinquedos/detalhes/:id", component: BrinquedoDetalheComponent },
  { path: "cadastro", component: CreateUsuarioComponent },
  { path: "login", component: LoginComponent },
  { path: "anuncios", component: HomeAnuncioComponent },
  { path: "AddAnuncio", component: CreateAnuncioComponent },
  { path: "MeusBrinquedos", component: ListBrinquedoComponent },
  { path: "AddBrinquedo", component: AddBrinquedoComponent },
  {
    path: "Addproposta",
    component: AddPropostaComponent,
    canActivate: [AuthGuard],
  },
  { path: "**", redirectTo: "" },
];
