import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiUrl } from 'app/models/ApiUrl';
import { Observable } from 'rxjs';
import { ServiceBaseService } from './serviceBase.service';
@Injectable()
export class ImagensService extends ServiceBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = ApiUrl.baseUrl + "imagens";
  }
  getFiles(idBrinquedo): Observable<any> {
    return this.http.get(`${this.baseUrl}/` + idBrinquedo);
  }
  upload(file: File, idUser: string): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();

    formData.append('file', file);
    formData.append('idUser', idUser);

    const req = new HttpRequest('POST', `${this.baseUrl}/`, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(req);
  }
}
