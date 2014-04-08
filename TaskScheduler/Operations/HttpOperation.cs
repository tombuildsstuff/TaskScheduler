﻿using System;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TaskScheduler.EventBus;
using TaskScheduler.Events;

namespace TaskScheduler.Operations
{
    public class HttpOperation : IOperation
    {
        private class HttpParameters
        {
            public string Url { get; set; }
            public string Body { get; set; }
            public string Verb { get; set; }
        }

        public void Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<HttpParameters>(parameters);
            Bus.Instance.Publish(new  ErrorThrownEvent
            {
                Exception = new Exception(parameters), 
                Id = Guid.NewGuid()
            });
              Bus.Instance.Publish(new  ErrorThrownEvent
            {
                Exception = new Exception(deserializedParameters.Url), 
                Id = Guid.NewGuid()
            });
            var client = WebRequest.Create(deserializedParameters.Url);
            
            var buf = string.IsNullOrEmpty(deserializedParameters.Body) ? new byte[0] : Encoding.UTF8.GetBytes(deserializedParameters.Body);

            client.Method = deserializedParameters.Verb;
            client.ContentType = "application/json";
            client.ContentLength = buf.Length;
            client.GetRequestStream().Write(buf, 0, buf.Length);
            client.Timeout = 10000;
            client.GetResponse();
        }
    }
}