import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrl } from "app/models/ApiUrl";
import { PagedResponse } from "app/models/PagedResponse";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ServiceBaseService } from "./serviceBase.service";

@Injectable({
  providedIn: "root",
})
export class AnuncioService extends ServiceBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = ApiUrl.baseUrl + "anuncios";
  }
}