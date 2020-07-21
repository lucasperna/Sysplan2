using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio1.Models
{
    public class DadosModel
    {
        [JsonProperty("cidade")]
        public string Cidade { get; set; }

        [JsonProperty("idade")]
        public decimal Idade { get; set; }
    }
}
