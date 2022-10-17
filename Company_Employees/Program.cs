using Company_Employees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


#region services
// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIIsIntegration(); 
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(CompanyEmployee.Presentation.AssemplyReference).Assembly);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLoggerService();
#endregion
//builder.Services.ConfigureRepositoryManager();


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigueExceptionHandler(logger);
// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region Midlleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello from the middleware component.");
    await next.Invoke();
    Console.WriteLine($"Logic after executing the next delegate in the Use method");

});

app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch logic in the Use method before the next delegate");
        await next.Invoke();
        Console.WriteLine("Map branch logic in the Use method after the next delegate");

    });

    builder.Run(async context =>
    {
        Console.WriteLine($"Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the map branch.");

    });
});

app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from the MapWhen branch.");
    });
});

app.Run(async context =>
{
    Console.WriteLine($"Writing the response to the client in the Run method");
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Hello from the midlleware.");
});

app.MapControllers();
#endregion
