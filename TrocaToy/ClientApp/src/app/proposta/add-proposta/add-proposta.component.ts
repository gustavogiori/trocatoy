import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { AnuncioService } from 'app/services/anuncio.service';
import { BrinquedoService } from 'app/services/brinquedo.service';
import { ImagensService } from 'app/services/imagens.service';
import { PropostaService } from 'app/services/proposta.service';
import { AddBase } from 'app/shared/CRUD/AddBase';
import { NotificationService } from 'app/shared/messages/notification.service';
let id = 0;
@Component({
  selector: 'app-add-proposta',
  templateUrl: './add-proposta.component.html',
  styleUrls: ['./add-proposta.component.scss']
})
export class AddPropostaComponent extends AddBase {
  constructor(
    protected brinquedoService: BrinquedoService,
    protected propostaService: PropostaService,
    protected notificationService: NotificationService,
    protected formBuilder: FormBuilder,
    protected imagensService: ImagensService,
    public route: ActivatedRoute,
    private anuncioService: AnuncioService,
  ) {
    super(propostaService, notificationService);
    this.titulo = "Nova Proposta";
  }
  anuncio;
  brinquedo;
  titulo;
  brinquedoForm: FormGroup;
  tiposProposta;
  brinquedos;
  propostaSelecionada;
  mudouTipoProposta(e) {
  }
  initializeForm() {
    this.brinquedoForm = this.formBuilder.group({

      IdBrinquedoProposto: this.formBuilder.control("", [
      ]),
      IdBrinquedoRequerido: this.formBuilder.control("", []),
      tipoProposta: this.formBuilder.control("", [
        Validators.required
      ]),
      Observacao: this.formBuilder.control("", [
        Validators.required,
        Validators.minLength(5),
      ]),
    });
  }
  ngOnInit() {
    this.initializeForm();
    this.brinquedoService.getAll(1, 100).subscribe((data) => {
      console.log(data);
      this.brinquedos = data.data;
    })
    this.route.params.subscribe((params: Params) => {
      id = params["id"];
      console.log(id);
      this.anuncioService.get(id).subscribe((data) => {
        this.anuncio = data;
        console.log(data);
        this.brinquedoForm.get('IdBrinquedoRequerido').setValue(this.anuncio.idBrinquedo);
        if (this.anuncio.tipoDisponibilidade === 2) {
          this.tiposProposta = [{ "id": 2, "descricao": 'Troca' }];
        }
        else if (this.anuncio.tipoDisponibilidade === 3) {
          this.tiposProposta = [{ "id": 1, "descricao": 'Doação' }, { "id": 2, "descricao": 'Troca' }];
        }
        else {
          this.tiposProposta = [{ "id": 1, "descricao": 'Doação' }];
        }
        console.log(this.anuncio);
      });
    });
  }

}
