using Newtonsoft.Json.Linq;
using Qlik.Engine;
using Qlik.Engine.Communication;
using Qlik.Sense.RestClient;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;

const string url = "https://xa4gk40bjgotquf.sg.qlikcloud.com";
const string appId = "a467284c-83a8-447a-bc80-36d5f2dddd2c";
const string key = "eyJhbGciOiJFUzM4NCIsImtpZCI6Ijk2YTUyNmM4LTkxYTUtNGNmNS1hMmQ3LWVmM2YzNTI3M2FhYSIsInR5cCI6IkpXVCJ9.eyJzdWJUeXBlIjoidXNlciIsInRlbmFudElkIjoiQXp2SlRoQlhPbi1VZGVTOXZVYlVGdjJmNEw3dnctS2oiLCJqdGkiOiI5NmE1MjZjOC05MWE1LTRjZjUtYTJkNy1lZjNmMzUyNzNhYWEiLCJhdWQiOiJxbGlrLmFwaSIsImlzcyI6InFsaWsuYXBpL2FwaS1rZXlzIiwic3ViIjoiNjYyNjhiZWIzMjIzNjY5MTcxNGUyMmEzIn0.F_ZwUbbFA0cFiaUrhrIAtVbFMUs16MsWMmf9VyGW9Tekk9rcC14dF8V7mwvhrdJGqWP76A2DIuhzozRONwKQZlmXKo_Hm79mf-p8lmidmXqexP9qB3USf_tnt4i_WWV4";

var location = QcsLocation.FromUri(url);

location.AsApiKey(key);
var fetchApp = location.App(appId);

using (var app = location.SessionAppFromApp(appId))
{
    Console.WriteLine(app.GetAppProperties().Title);
    var properties = app.GetAppProperties();

    IEnumerable<Connection> connections = app.GetConnections();
    var script = app.GetScriptEx();
    Console.WriteLine(script);
    var appLayout = app.GetAppLayout();
    Console.WriteLine(appLayout);
    Console.WriteLine(appLayout.FileName);
    
}

var restClient = new RestClient(location.ServerUri);

restClient.AsApiKeyViaQcs(key);
//Console.WriteLine(restClient.Get("/qrs/about"));
//Console.WriteLine(restClient.Get($"/qrs/app/{appId}"));

string id = restClient.User.Id;
string directory = restClient.User.Directory;
var rsp = restClient.GetAsync($"/api/v1/apps/{appId}/data/metadata").Result;
Console.WriteLine(rsp);

// To export the existing APP into another
/*
var outputFile = "MyAppCopy.qvf";
var rsp = restClient.PostHttpAsync($"/api/v1/apps/{appId}/export", throwOnFailure: false).Result;
Console.WriteLine($"Request returned status code: {(int)rsp.StatusCode} ({rsp.StatusCode})");
if (rsp.StatusCode == HttpStatusCode.Created)
{
    var downloadPath = rsp.Headers.Location.OriginalString;
    Console.WriteLine("Download file location: " + downloadPath);

    Console.Write("Downloading... ");

    var stream = restClient.GetStream(downloadPath);
    using (var fileStream = File.OpenWrite(outputFile))
    {
        stream.CopyTo(fileStream);
    }

    Console.WriteLine("Done.");
    Console.WriteLine($"App export complete. Output file: {outputFile} ({new FileInfo(outputFile).Length} bytes)");
}*/

