import { AbstractControl } from "@angular/forms";

export class Validations {
  static EmailsCombinam(controle: AbstractControl) {
    console.log('validando email');
    let email = controle.get('email').value;
    let confirmarEmail = controle.get('emailConfirmation').value;
console.log(email);
console.log(confirmarEmail);
    if (email === confirmarEmail) return null;

    controle.get('emailConfirmation').setErrors({ emailsNotMatch: true });
  }
  static equalsTo(group: AbstractControl): { [key: string]: boolean } {
    const email = group.get("email");
    const emailConfirmation = group.get("emailConfirmation");
    if (!email || !emailConfirmation) {
      return undefined;
    }
    if (email.value !== emailConfirmation.value) {
      return { emailsNotMatch: true };
    }
    return undefined;
  }
  static ValidaCpf(controle: AbstractControl) {
    let cpf:string = controle.value;
    console.log(cpf);
    cpf=cpf.replace('.','').replace('-','');
    console.log(cpf);

    let soma: number = 0;
    let resto: number;
    let valido: boolean;

    const regex = new RegExp('[0-9]{11}');

    if (
      cpf == '00000000000' ||
      cpf == '11111111111' ||
      cpf == '22222222222' ||
      cpf == '33333333333' ||
      cpf == '44444444444' ||
      cpf == '55555555555' ||
      cpf == '66666666666' ||
      cpf == '77777777777' ||
      cpf == '88888888888' ||
      cpf == '99999999999' ||
      !regex.test(cpf)
    )
      valido = false;
    else {
      for (let i = 1; i <= 9; i++)
        soma = soma + parseInt(cpf.substring(i - 1, i)) * (11 - i);
      resto = (soma * 10) % 11;

      if (resto == 10 || resto == 11) resto = 0;
      if (resto != parseInt(cpf.substring(9, 10))) valido = false;

      soma = 0;
      for (let i = 1; i <= 10; i++)
        soma = soma + parseInt(cpf.substring(i - 1, i)) * (12 - i);
      resto = (soma * 10) % 11;

      if (resto == 10 || resto == 11) resto = 0;
      if (resto != parseInt(cpf.substring(10, 11))) valido = false;
      valido = true;
    }

    if (valido) {
      console.log('valido');
      return null;
    }
console.log('invalido');
    return { cpfInvalido: true };
  }


}
