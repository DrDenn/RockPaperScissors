using System;
using Microsoft.Extensions.Configuration;

namespace RpsWebsite.Services
{
    public interface IRpsServerClient
    {
        string Endpoint { get; }
    }

    /// <summary>
    /// Custom middleware for integration with the RpsServer Apis.
    /// </summary>
    public sealed class RpsServerClient : IRpsServerClient
    {
        private string _endpoint;

        public RpsServerClient(IConfiguration config)
        {
            _endpoint = config["RpsServerEndpoint"];
        }

        public string Endpoint => _endpoint;
    }
}
