import {
  Component,
  OnInit,
  Input,
  ContentChild,
  AfterContentInit,
  ContentChildren,
} from "@angular/core";
import { NgModel, FormControlName } from "@angular/forms";

@Component({
  selector: "app-input",
  templateUrl: "./input.component.html",
})
export class InputComponent implements OnInit, AfterContentInit {
  @Input() label: string;
  @Input() errorMessage: string;

  input: any;
  @ContentChildren(NgModel) model: NgModel;
  @ContentChildren(FormControlName) control: FormControlName;
  constructor() {}

  ngOnInit() {}

  ngAfterContentInit() {
    this.input = this.control || this.model;
    if (this.input === undefined) {
      console.log("erro");
      throw new Error(
        "Esse componente precisa ser usado com uma diretiva ngModel ou formControlName"
      );
    } else {
      this.input = this.input._results[0];
    }
  }

  hasSuccess(): boolean {
    var teste = this.input.valid && (this.input.dirty || this.input.touched);

    return teste;
  }

  hasError(): boolean {
    var teste = this.input.invalid && (this.input.dirty || this.input.touched);
    return teste;
  }
}
{
}
