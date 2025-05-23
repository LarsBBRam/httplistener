using System.Net;

namespace httplistener.ServerExamples;

public class SimpleHttpServer : IDisposable
{
    private readonly HttpListener _listener = new();

    private async void ListenAsync()
    {
        _listener.Prefixes.Add("http://localhost:9001");

        _listener.Start();

        var requestContext = await _listener.GetContextAsync();

        Console.WriteLine(requestContext.Request.HttpMethod);
        Console.WriteLine(requestContext.Request.RawUrl);
    }

    public void Dispose() => _listener.Close();
}