﻿using System;
using System.Net;
using Newtonsoft.Json;

namespace TaskScheduler.Operations
{
    public class HttpOperation : IOperation
    {
        public ResponseStatus Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<HttpParameters>(parameters);
            var client = WebRequest.Create(deserializedParameters.Url);
            client.Method = deserializedParameters.Verb ?? "POST";

            if (deserializedParameters.TimeoutInSeconds.HasValue)
                client.Timeout = (int) TimeSpan.FromSeconds(deserializedParameters.TimeoutInSeconds.Value).TotalMilliseconds;
            
            client.ContentLength = 0;
            var response = (HttpWebResponse)client.GetResponse();
            
            return IsSuccessful(response) ? ResponseStatus.Finished : ResponseStatus.FailedToComplete;
        }

        private bool IsSuccessful(HttpWebResponse response)
        {
            var intStatusCode = (int)response.StatusCode;
            return intStatusCode >= 200 && intStatusCode < 300;
        }

        private class HttpParameters
        {
            public int? TimeoutInSeconds { get; set; }

            public string Url { get; set; }

            public string Verb { get; set; }
        }
    }
}
