import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ServiceBaseService } from "./serviceBase.service";
import { ApiUrl } from "../models/ApiUrl";

@Injectable({
  providedIn: "root",
})
export class UsuarioService extends ServiceBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = ApiUrl.baseUrl + "Usuarios";
  }
}
