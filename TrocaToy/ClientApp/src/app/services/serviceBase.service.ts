import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { PagedResponse } from "app/models/PagedResponse";
import { PaginationFilter } from "app/models/PaginationFilter";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
@Injectable({
  providedIn: "root",
})
export class ServiceBaseService {
  public baseUrl: string;
  getAll(pageNumber: number, pageSize: number): Observable<PagedResponse> {
    return this.http
      .get<PagedResponse>(
        `${this.baseUrl}?PageNumber=${pageNumber}&PageSize=${pageSize}`
      )
      .pipe(map((result) => result));
  }
  getAllNotPagination(): Observable<any> {
    return this.http
      .get<PagedResponse>(`${this.baseUrl}`)
      .pipe(map((result) => result));
  }
  filter(campo, valor): Observable<any> {
    if (valor) {
      return this.http.get<any>(`${this.baseUrl}/${campo}/${valor}`);
    } else {
      return this.getAll(10, 1);
    }
  }
  get(id): Observable<any> {
    return this.http
      .get<any>(`${this.baseUrl}/${id}`)
      .pipe(map((result) => result));
  }
  create(data) {
    const headers = { "content-type": "application/json" };
    const body = JSON.stringify(data);
    console.log(body);
    return this.http.post(this.baseUrl, body, { headers: headers });
  }

  update(id, data) {
    return this.http.put(`${this.baseUrl}/${id}`, data);
  }

  delete(id) {
    let url = `${this.baseUrl}/${id}`;
    let retorno = this.http.delete(url);
    return retorno;
  }

  deleteAll() {
    return this.http.delete(this.baseUrl);
  }

  constructor(protected http: HttpClient) {}
}
