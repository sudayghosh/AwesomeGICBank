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
    public class InterestRuleService : IInterestRuleService
    {
        private readonly IRestClient _restClient;
        private readonly string _apiUrl;

        public InterestRuleService(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");

            //var options = new RestClientOptions(_apiUrl)
            //{
            //    ThrowOnAnyError = true
            //};
            _restClient = new RestClient(_apiUrl);
        }

        public async Task<InterestRuleRespDto?> UpsertInterestRule(InterestRuleReqDto reqDto)
        {
            InterestRuleRespDto? rule = new();
            var request = new RestRequest("interestrule/", Method.Post);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddBody(reqDto);
            var response = await _restClient.ExecutePostAsync(request);
            if (!response.IsSuccessful)
            {
                rule.ErrorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response?.Content);
            }
            else if (response.IsSuccessful && !string.IsNullOrEmpty(response?.Content))
            {
                rule.Rules = JsonConvert.DeserializeObject<List<InterestRuleDetailsRespDto>>(response?.Content);
            }
            return rule;
        }
    }
}
