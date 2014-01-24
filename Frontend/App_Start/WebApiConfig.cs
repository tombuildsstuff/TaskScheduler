﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Frontend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

            config.Routes.MapHttpRoute("apiendpoint",
                routeTemplate: "api/{controller}");
        }
    }
}
