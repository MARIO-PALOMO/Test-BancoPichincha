using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco_dto.Respuestas
{
    public class BaseResponse
    {
        [JsonProperty("codigo")]
        public int Codigo { get; set; }

        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }
    }

    public class ResponseIntegration<T> : BaseResponse
    {
        [JsonProperty("datos")]
        public T Datos { get; set; }
    }
}
