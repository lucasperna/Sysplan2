using Exercicio1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio1
{
    public class Formatacao
    {
        public ResultadoModel CreateObjects(string texto)
        {
            var dados = texto.Split("\n")
                .Select(v => v.Split(',')).Select(l => new PessoaModel
                {
                    Nome = l[0],
                    Cidade = RemoveAccents(l[1].ToLower()),
                    Idade = Convert.ToInt32(l[2])
                }).ToList();
            
            List<DadosModel> avg = new List<DadosModel>();
            avg = dados.GroupBy(g => g.Cidade)
                .Select(s => new DadosModel
                {
                    Idade = Math.Round(s.Average(a => a.Idade), 2),
                    Cidade = s.Key
                }).ToList();

            var average = new MediaModel { Media = avg };
            var json = JsonConvert.SerializeObject(average);
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://zeit-endpoint.brmaeji.now.sh/api/avg")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var contentString = new StringContent(json, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = client.PostAsync("", contentString).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            var msg = (MessageModel)JsonConvert.DeserializeObject(response, typeof(MessageModel));
            var sendModel = new ResultadoModel { Response = msg, MediaValorList = avg };
            return sendModel;
        }

        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
    }
}
