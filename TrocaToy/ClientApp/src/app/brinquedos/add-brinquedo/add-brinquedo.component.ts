import { HttpEventType, HttpResponse } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BrinquedoService } from "app/services/brinquedo.service";
import { ImagensService } from "app/services/imagens.service";
import { AddBase } from "app/shared/CRUD/AddBase";
import { NotificationService } from "app/shared/messages/notification.service";
import { Observable } from "rxjs";

@Component({
  selector: "app-add-brinquedo",
  templateUrl: "./add-brinquedo.component.html",
  styleUrls: ["./add-brinquedo.component.css"],
})
export class AddBrinquedoComponent extends AddBase {
  brinquedoForm: FormGroup;

  constructor(
    protected brinquedoService: BrinquedoService,
    protected notificationService: NotificationService,
    protected formBuilder: FormBuilder,
    protected imagensService: ImagensService
  ) {
    super(brinquedoService, notificationService);
    this.titulo = "Cadastro de brinquedos";
  }
  adicionouItem = false;
  idUser = "";
  titulo = "";
  selectedFiles: FileList;
  progressInfos = [];
  message = '';
  fileInfos: Observable<any>;
  initializeForm() {
    this.brinquedoForm = this.formBuilder.group({
      nome: this.formBuilder.control("", [
        Validators.required,
        Validators.minLength(5),
      ]),
      marca: this.formBuilder.control("", [
        Validators.compose([Validators.required]),
      ]),
      novo: this.formBuilder.control(""),
    });
  }
  selectFiles(event) {
    this.progressInfos = [];
    this.selectedFiles = event.target.files;
  }

  upload(idx, file) {
    this.progressInfos[idx] = { value: 0, fileName: file.name };

    this.imagensService.upload(file, this.idUser).subscribe(
      event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progressInfos[idx].value = Math.round(100 * event.loaded / event.total);
        } else if (event instanceof HttpResponse) {
          this.progressInfos[idx].value = 0;
          this.imagensService.getFiles(this.idUser).subscribe(
            response => {
              console.log(response);
              this.fileInfos = response;
            }
          );
          console.log(this.fileInfos);
        }
      },
      err => {
        this.progressInfos[idx].value = 0;
        this.message = 'Could not upload the file:' + file.name;
      });
  }

  uploadFiles() {
    this.message = '';

    for (let i = 0; i < this.selectedFiles.length; i++) {
      this.upload(i, this.selectedFiles[i]);
    }
  }
  afterSave(response) {
    this.adicionouItem = true;
    console.log(response);
    this.idUser = response.id;
  }
  ngOnInit() {
    this.initializeForm();
  }
}
