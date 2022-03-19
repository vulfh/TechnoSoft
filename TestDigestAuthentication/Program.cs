using DigestAuthenticationClient;
using System;
using System.Threading.Tasks;

namespace TestDigestAuthentication
{
    class Program
    {
        static async Task Main(string[] args)
        {

            AuthenticatedClient client = new AuthenticatedClient("http://localhost/api", "user", "password");
            var response = await client.PostJson("upload",new { Foo = "foo", Bar = "bar", Quiz = "Quiz" });
            Console.WriteLine("Hello World!");
        }
    }
}
