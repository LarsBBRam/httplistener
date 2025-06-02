using System.Threading.Tasks;
using httplistener.ServerExamples;

namespace httplistener;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();

        using var server = new SimpleHttpServer();


        var response = await client.GetAsync("http://localhost:9001/duckimages");
        Console.WriteLine(response);

        var fileServer = new SimpleFileResourceServer("http://localhost:9002/", "./wwwroot");

        fileServer.Start();

        Console.WriteLine("Server running pless any button to continue");
        Console.ReadLine();
        fileServer.Stop();


    }
}
