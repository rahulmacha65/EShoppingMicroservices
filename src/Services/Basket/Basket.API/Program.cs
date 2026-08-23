var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddCarter();
builder.Services.AddMediatR(ofg =>
{
    ofg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    ofg.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    ofg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();

// configure HTTP request pipeline
app.MapCarter();
app.Run();
