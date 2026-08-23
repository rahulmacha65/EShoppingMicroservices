var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddCarter();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
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
