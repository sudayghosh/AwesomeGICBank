using AwesomeGIC.Bank.UI.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.UI.Service
{
    public class AccountService : IAccountService
    {
        private readonly IRestClient _restClient;
        private readonly string _apiUrl;

        public AccountService(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");
            _restClient = new RestClient(_apiUrl);
        }

        public async Task<AccountRespDto?> UpsertAccount(AccountReqDto reqDto)
        {
            var request = new RestRequest("account/", Method.Post);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddBody(reqDto);
            var response = await _restClient.ExecutePostAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK 
                && !string.IsNullOrEmpty(response?.Content))
            {
                var account = JsonConvert.DeserializeObject<AccountRespDto>(response?.Content);
                return account;
            }
            return null;
        }
    }
}
