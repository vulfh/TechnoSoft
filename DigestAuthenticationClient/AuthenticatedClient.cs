using System;
using System.Net;

namespace DigestAuthenticationClient
{
    public class AuthenticatedClient:IDisposable
    {
        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string BaseUrl { get; private set; }

        private WebClient _client;
        public AuthenticatedClient(string baseUrl,string username,string password)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
            UserName = username;
            Password = password;
            BaseUrl = baseUrl;
          
        }
        public string PostJson(string resource,string payload) 
        {
            try
            {
                using var client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Credentials = new NetworkCredential(UserName, Password);
                return client.UploadString(CombineResource(resource),payload);
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

        private string CombineResource(string resource)
        {
            if (BaseUrl.EndsWith("/") || resource.StartsWith("/"))
                return $"{BaseUrl}{resource}";
            else
                return $"{BaseUrl}/{resource}";
        }
    }
}
