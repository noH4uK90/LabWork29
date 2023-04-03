using LabWork28;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var application = builder.Build();
ConfigureApp(application);
await using var scope = application.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
await context.Database.EnsureCreatedAsync();
await application.RunAsync();

void RegisterServices(IServiceCollection services)
{
    services.AddDbContext<ProductsDbContext>();
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}
