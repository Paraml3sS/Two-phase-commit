using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AggregatorCompany.Api;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AggregatorCompany.Api
{
    public class TransactionManager
    {
        private readonly IHttpClientFactory _clientFactory;

        public TransactionManager(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> Execute(TransactionInfo info)
        {                
            var prepareSuccess = await Prepare(info);
            var transactionSuccess = prepareSuccess 
                ? await Commit(info)
                : await Rollback(info);
            
            return transactionSuccess;
        }
        
        private async Task<bool> MakeRequests(TransactionInfo info)
        {
            var http = _clientFactory.CreateClient();
            var calls = info.ApiCalls.Select(ac => http.PostAsJsonAsync(ac.Key, ac.Value));
            var results = await Task.WhenAll(calls);
            var success = results.All(r => r.IsSuccessStatusCode);
            return success;
        }
        
        private async Task<bool> Prepare(TransactionInfo info) => await MakeRequests(info);
        
        private async Task<bool> Commit(TransactionInfo info)
        {
            info.ApiCalls = info.ApiCalls.ToDictionary(ac => $"{ac.Key}/commit", ac => (object) info.TransactionId);
            return await MakeRequests(info);
        }
        
        private async Task<bool> Rollback(TransactionInfo info)
        {
            info.ApiCalls = info.ApiCalls.ToDictionary(ac => $"{ac.Key}/rollback", ac => (object) info.TransactionId);
            return await MakeRequests(info);
        }
    }

}