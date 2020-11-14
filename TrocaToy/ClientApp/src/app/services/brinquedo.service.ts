import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrl } from "app/models/ApiUrl";
import { ServiceBaseService } from "./serviceBase.service";

@Injectable()
export class BrinquedoService extends ServiceBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = ApiUrl.baseUrl + "brinquedos";
  }
}
