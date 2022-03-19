using RestSharp;
using RestSharp.Authenticators.Digest;
using RestSharp.Serializers.Utf8Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DigestAuthenticationClient
{
    public class AuthenticatedClient:IDisposable
    {
        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string BaseUrl { get; private set; }

        private RestClient _client;
        public AuthenticatedClient(string baseUrl,string username,string password)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
            UserName = username;
            Password = password;
            BaseUrl = baseUrl;
            _client = new RestClient(baseUrl)
            {
                Authenticator = new DigestAuthenticator(string.IsNullOrWhiteSpace(username) ? UserName : username,
                                                       string.IsNullOrWhiteSpace(password) ? Password : password)
            };


        }
        public async Task<string> PostJson<T>(string resource,T payload) where T:class
        {
            try
            {
                var request = new RestRequest(resource, Method.Post);
                request.RequestFormat = DataFormat.Json;
                var restRequest = request.AddJsonBody(payload);
                var response = await _client.PostAsync(restRequest);
                return response.Content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed post {payload} to {resource}.", ex);
            }

        }

        public void Dispose()
        {
            try
            {
                _client.Dispose();
            }
            finally { }
        }
    }
}
