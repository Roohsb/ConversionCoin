using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using Hellang.Middleware.ProblemDetails;
using Newtonsoft.Json;

namespace Conversion.Services
{
    public class ConvertServices
    {
        private readonly IRestClient _restclient;

        public ConvertServices ()
            {
             _restclient = new RestClient ();
            }
        public async Task<dynamic> GetRateCoin() //Use the available api to check the rates values ​​so you can access using dynamic.
        {

            var request = new RestRequest("http://api.exchangeratesapi.io/v1/latest?base=EUR", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("access_key", "dabd5742761e0aa75162cb1457b95dbb");


            var response = await _restclient.ExecuteAsync<dynamic>(request);


            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ProblemDetailsException(new ProblemCustomDetails
                {

                    Type = _restclient.BaseUrl.OriginalString.ToString() + request.Resource.ToString(),
                    Title = "Error fetching currency rate.",
                    Status = (int)response.StatusCode,
                    AdditionalInfo = JsonConvert.DeserializeObject(response.Content.ToString())


                });
            }


            return response.Data;

        }
    }
}
