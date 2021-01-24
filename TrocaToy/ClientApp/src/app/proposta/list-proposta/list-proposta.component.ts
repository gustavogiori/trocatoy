import { Component, OnInit } from '@angular/core';
import { PropostaService } from 'app/services/proposta.service';

@Component({
  selector: 'app-list-proposta',
  templateUrl: './list-proposta.component.html',
  styleUrls: ['./list-proposta.component.scss']
})
export class ListPropostaComponent implements OnInit {

  constructor(protected propostaService: PropostaService) { }
  title;
  propostas;
  aceitar(id) {
    console.log(id);
    this.propostaService.aceitarProposta(id).subscribe((data)=>{
      this.getAll();
    });
  }
  rejeitar(id) {
    this.propostaService.rejeitarProposta(id).subscribe((data)=>{
      this.getAll();
    });
  }
  getAll(){
    this.propostaService.getAll(1, 100).subscribe((data) => {
      this.propostas = data.data;
      console.log(this.propostas);
    });
  }
  ngOnInit() {
    this.title = "Propostas";
    this.getAll();
  }

}
