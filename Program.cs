using System.Threading.Tasks;
using httplistener.ServerExamples;

namespace httplistener;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();

        using var server = new SimpleHttpServer();

        var response = await client.GetStringAsync("http://localhost:9001/Hello-World!/");
        Console.WriteLine(response);
    }
}
