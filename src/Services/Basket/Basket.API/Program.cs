var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.

var app = builder.Build();

// configure HTTP request pipeline

app.Run();
