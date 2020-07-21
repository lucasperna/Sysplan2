using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio1.Models
{
    public class MediaModel
    {
        [JsonProperty("medias")]
        public List<DadosModel> Media { get; set; }
    }
}
