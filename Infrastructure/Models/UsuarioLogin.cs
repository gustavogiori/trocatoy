using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Models
{

    public class UsuarioLogin
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public int NivelPermissao { get; private set; }
        public void SetNivelPermissao(int permissao)
        {

            var valueseEnum = Enum.GetValues(typeof(NivelPermissaoEnum)).Cast<int>();

            if (valueseEnum.Contains(permissao))
                NivelPermissao = permissao;
            else
                NivelPermissao = GConvert.ToInt32(NivelPermissaoEnum.User);

        }

    }
}
