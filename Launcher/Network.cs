using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
internal class Network
{
    static string CookiePath => Path.Combine(Paths.BasePath, "cookie.txt");
    static string UserAgentPath = Path.Combine(Paths.BasePath, "useragent.txt");

    static HttpClient Client { get; } = new();

    static string? _cookie;
    static string Cookie
    {
        get
        {
            _cookie ??= File.ReadAllText(CookiePath);
            return _cookie;
        }
    }

    public static string Get(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("cookie", $"session={Cookie}");
        request.Headers.TryAddWithoutValidation("User-Agent", File.ReadAllText(UserAgentPath));

        var response = Client.Send( request );
        var responseTask = response.Content.ReadAsStringAsync();
        responseTask.Wait();
        return responseTask.Result;
    }

    public static string Post(string url, int level, string answer)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("cookie", $"session={Cookie}");
        request.Headers.TryAddWithoutValidation("User-Agent",File.ReadAllText(UserAgentPath));
        request.Content = new FormUrlEncodedContent(
            new[]
            {
                new KeyValuePair<string, string>("level", level.ToString()),
                new KeyValuePair<string, string>("answer", answer)
            });

        var response = Client.Send( request );
        var responseTask = response.Content.ReadAsStringAsync();
        responseTask.Wait();
        return responseTask.Result;
    }
}
