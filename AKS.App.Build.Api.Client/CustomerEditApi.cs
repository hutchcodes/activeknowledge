using AKS.Common.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Build.Api.Client
{
    public class CustomerEditApi
    {
        readonly string _aksBuildApiBaseUrl;

        public CustomerEditApi(IConfiguration configuration)
        {
            _aksBuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
        }        

        public async Task<CustomerEdit> GetCustomer(Guid customerId)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("customeredit/{customerId}", Method.GET);
            request.AddUrlSegment("customerId", customerId);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<CustomerEdit>(request);
            var customer = response.Data;
            return customer;
        }

        public async Task<CustomerEdit> UpdateCustomer(CustomerEdit customerEdit)
        {
            var client = new RestClient(_aksBuildApiBaseUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("customeredit", Method.POST);
            request.AddJsonBody(customerEdit);

            // easily add HTTP Headers
            request.AddHeader("header", "value");

            var response = await client.ExecuteTaskAsync<CustomerEdit>(request);
            var customer = response.Data;
            return customer;
        }
    }
}
