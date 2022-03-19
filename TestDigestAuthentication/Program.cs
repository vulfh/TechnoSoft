using DigestAuthenticationClient;
using System;
using System.Threading.Tasks;

namespace TestDigestAuthentication
{
    class Program
    {
        static void Main(string[] args)
        {

            AuthenticatedClient client = new AuthenticatedClient("http://localhost/api", "user", "password");
            var response = client.PostJson("upload","{'Foo':'Foo'}");
            Console.WriteLine(response);
            Console.ReadLine();
        }
    }
}
