import { Routes } from "@angular/router";

import { BrinquedosComponent } from "./brinquedos/brinquedos.component";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { AddPropostaComponent } from "./proposta/add-proposta/add-proposta.component";
import { CreateUsuarioComponent } from "./usuario/create-usuario/create-usuario.component";
import { AuthGuard } from "./_helper/auth.guard";

export const ROUTES: Routes = [
  { path: "", component: BrinquedosComponent },
  { path: "brinquedos", component: BrinquedosComponent },
  { path: "cadastro", component: CreateUsuarioComponent },
  { path: "login", component: LoginComponent },
  { path: "Addproposta", component: AddPropostaComponent, canActivate: [AuthGuard]  },
  { path: "**", redirectTo: "login" },
];
