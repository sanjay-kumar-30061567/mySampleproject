using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.Digest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI
{
    public class LoyalityConnector
    {
        private readonly string _url;
        private readonly string _userId;
        private readonly string _password;

        public LoyalityConnector(string url, string userId, string password)
        {
            _url = url;
            _userId = userId;
            _password = password;
        }

        public string WebAPICall(bool isCap = false)
        {
            var client = new RestClient
            {
                BaseUrl = new System.Uri(_url),
                Authenticator = isCap ? new HttpBasicAuthenticator(_userId, _password) : new DigestAuthenticator(_userId, _password),

            };
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);          

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return response.Content;
        }
        public string WebAPICall(string payload,bool isCap = false)
        {
            var client = new RestClient
            {
                BaseUrl = new System.Uri(_url),
                Authenticator = isCap ? new HttpBasicAuthenticator(_userId, _password) : new DigestAuthenticator(_userId, _password),

            };
            RestRequest request = new RestRequest(Method.POST);            
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", payload, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            return response.Content;
        }

    }
}
