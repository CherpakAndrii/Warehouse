using Microsoft.AspNetCore.Hosting;
using System.Net;
using WebApi;

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();


var builder = WebApplication.CreateBuilder(args);

//Change max connections from .NET to a remote service default: 2
ServicePointManager.DefaultConnectionLimit = 65000;
//Bump up the min threads reserved for this app to ramp connections faster - minWorkerThreads defaults to 4, minIOCP defaults to 4
ThreadPool.SetMinThreads(100, 100);
//Turn off the Expect 100 to continue message - 'true' will cause the caller to wait until it round-trip confirms a connection to the server
ServicePointManager.Expect100Continue = false;
//Can decreas overall transmission overhead but can cause delay in data packet arrival
ServicePointManager.UseNagleAlgorithm = false;

var startup = new Startup();

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();