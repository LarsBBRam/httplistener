using System.Net;
using System.Text;

namespace httplistener.ServerExamples;

public class SimpleFileResourceServer
{
    private HttpListener _listener;

    private string _fileFolder;

    public SimpleFileResourceServer(string uriPrefix, string folderName)
    {
        _listener = new();
        _listener.Prefixes.Add(uriPrefix);
        _fileFolder = folderName;
    }

    private async void ProcessRequestAsync(HttpListenerContext context)
    {
        var filename = Path.GetFileName(context.Request.RawUrl);
        var filepath = Path.Combine(_fileFolder, filename);

        byte[] responseMessage;

        if (!File.Exists(filepath))
        {
            Console.WriteLine($"Resource not found {filepath}");
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            responseMessage = Encoding.UTF8.GetBytes("Sorry, the resource you're after is not  available.");
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            responseMessage = await File.ReadAllBytesAsync(filepath);
        }

        context.Response.ContentLength64 = responseMessage.Length;

        using var outputStream = context.Response.OutputStream;
        await outputStream.WriteAsync(responseMessage);

    }

    public async void Start()
    {
        _listener.Start();

        while (true)
        {
            var context = await _listener.GetContextAsync();
            Task.Run(() => ProcessRequestAsync(context));
        }
    }

    public void Stop() => _listener.Stop();
}