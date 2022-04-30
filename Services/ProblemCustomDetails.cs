using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Conversion.Services
{
    internal class ProblemCustomDetails : ProblemDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public string Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public string Title { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 4)]
        public string Detail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 5)]
        public string Instance { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 6)]
        public dynamic AdditionalInfo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public IDictionary<string, object> Extensions { get; }

    }
}