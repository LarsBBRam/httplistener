using System.Net;
using System.Text;

namespace httplistener.ServerExamples;

public class SimpleHttpServer : IDisposable
{
    private readonly HttpListener _listener = new();

    public SimpleHttpServer() => ListenAsync();

    private async void ListenAsync()
    {
        _listener.Prefixes.Add("http://localhost:9001/");

        _listener.Start();

        var requestContext = await _listener.GetContextAsync();

        Console.WriteLine(requestContext.Request.HttpMethod);
        Console.WriteLine(requestContext.Request.RawUrl);

        var responseMessage = new StringBuilder();

        if (requestContext.Request.RawUrl == "Hello-World") responseMessage.Append("Hello, World!");
        else responseMessage.Append($"You asked for the following resource {requestContext.Request.RawUrl}, using the following  http Method {requestContext.Request.HttpMethod}");

        requestContext.Response.ContentLength64 = Encoding.UTF8.GetByteCount(responseMessage.ToString());
        requestContext.Response.StatusCode = (int)HttpStatusCode.OK;

        using var outputStream = requestContext.Response.OutputStream;
        using var streamWriter = new StreamWriter(outputStream);

        await streamWriter.WriteAsync(responseMessage);
    }

    public void Dispose() => _listener.Close();
}