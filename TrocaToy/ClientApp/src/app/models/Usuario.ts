import { Endereco } from "./Endereco";

export class Usuario {
  Id: string;
  Nome: string;
  Cpf: string;
  Rg: string;
  Telefone: string;
  Email: string;
  Senha: string;
  isLoggedIn:boolean;
  Role:string;
  Endereco: Endereco[];
}
