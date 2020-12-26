using Infrastructure.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Models;

namespace TrocaToy.Service
{
    public class ImgurService
    {
        /// <summary>
        /// Cria uma imagem no site imgur
        /// </summary>
        /// <returns>Retorna a url da imagem criada</returns>
        public string UploadFile(string base64File)
        {
            var client = new RestClient("https://api.imgur.com/3/image");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Client-ID 7add5f6daea99bb");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", base64File);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var retorno = JsonService<ImgUrResponse>.GetObject(response.Content);
            return retorno.data.link;
        }
    }
}
