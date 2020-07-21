using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio1.Models
{
    public class MessageModel
    {
        [JsonProperty("sucesso")]
        public bool Sucess { get; set; }

        [JsonProperty("mensagem")]
        public string Message { get; set; }
    }
}
