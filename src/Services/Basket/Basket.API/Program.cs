//using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddCarter();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//Decorating using Scrutor libaray
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// manually decorating cachebasketrepository with distributed caching
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var basketRepository = provider.GetRequiredService<BasketRepository>();
//    var distributedCache = provider.GetRequiredService<IDistributedCache>();
//    return new CacheBasketRepository(basketRepository, distributedCache);
//});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddMediatR(ofg =>
{
    ofg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    ofg.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    ofg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
    config.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();
var app = builder.Build();

// configure HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
