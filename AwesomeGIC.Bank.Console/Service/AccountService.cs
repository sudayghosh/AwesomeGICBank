using AwesomeGIC.Bank.UI.Dto;
using AwesomeGIC.Bank.UI.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace AwesomeGIC.Bank.UI.Service
{
    public class AccountService : IAccountService
    {
        private readonly IRestClient _restClient;
        private readonly string _apiUrl;

        public AccountService(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");

            //var options = new RestClientOptions(_apiUrl)
            //{
            //    ThrowOnAnyError = true
            //};
            _restClient = new RestClient(_apiUrl);
        }

        public async Task<AccountRespDto?> UpsertAccount(AccountReqDto reqDto)
        {
            AccountRespDto? account = null;
            var request = new RestRequest("account/", Method.Post);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("ApiKey", Helper.API_KEY);
            request.AddBody(reqDto);
            var response = await _restClient.ExecutePostAsync(request);
            if (!response.IsSuccessful)
            {
                account = new()
                {
                    ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response?.Content)
                };
            }
            else if (response.IsSuccessful && !string.IsNullOrEmpty(response?.Content))
            {
                account = JsonConvert.DeserializeObject<AccountRespDto>(response?.Content);
            }
            return account;
        }

        public async Task<AccountRespDto?> GetAccountStatement(AccountStatementReqDto reqDto)
        {
            AccountRespDto? account = null;
            var request = new RestRequest("account/transactions", Method.Post);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("ApiKey", Helper.API_KEY);
            request.AddBody(reqDto);
            var response = await _restClient.ExecutePostAsync(request);
            if (!response.IsSuccessful)
            {
                account = new()
                {
                    ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response?.Content)
                };
            }
            else if (response.IsSuccessful && !string.IsNullOrEmpty(response?.Content))
            {
                account = JsonConvert.DeserializeObject<AccountRespDto>(response?.Content);
            }
            return account;
        }
    }
}
