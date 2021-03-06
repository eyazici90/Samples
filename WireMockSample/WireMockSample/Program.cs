﻿using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMockSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = FluentMockServer.Start(8080);
            Console.Out.WriteLine("Server started on port " + "8080");

            server
              .Given(Request.Create().WithPath("/*"))
              .RespondWith(Response.Create()
                  .WithStatusCode(200)
                  .WithHeader("Content-Type", "application/json")
                  .WithBody(@"{ ""result"": ""Hello From WireMock""}")
                  .WithDelay(TimeSpan.FromSeconds(1))
             );

            Console.ReadLine();
        }
    }
}
