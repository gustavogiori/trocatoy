import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Injectable } from '@angular/core';
import { ApiUrl } from 'app/models/ApiUrl';
import { ServiceBaseService } from './serviceBase.service';

@Injectable({
  providedIn: "root",
})
export class PropostaService extends ServiceBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = ApiUrl.baseUrl + "propostas";
  }
  rejeitarProposta(id) {
    let url = `${this.baseUrl}/RejeitarProposta/`;
    console.log(url);
    return this.http.put(url, { "id": id });
  }
  aceitarProposta(id) {
    let url = `${this.baseUrl}/AceitarProposta/`;
    console.log(url);
    return this.http.put(url, { "id": id });
  }
}
