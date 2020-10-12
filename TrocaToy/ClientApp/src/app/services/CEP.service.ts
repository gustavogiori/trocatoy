import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class CEPService {
  constructor(protected http: HttpClient) {}

  public GetCep(cepNumber) {
   return this.http.get(`https://viacep.com.br/ws/${cepNumber}/json/`);
  }

}
