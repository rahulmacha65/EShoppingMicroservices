using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//Add services to DI
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimitOptions =>
{
    rateLimitOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(15);
        options.PermitLimit = 7;
    });
});

var app = builder.Build();

//Configure the HTTP request pipeline

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
