
using TestPipeline;
using TestPipeline.Extensions;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

//builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

builder.Setup();

builder.SetupConfigurations();

builder.ConfigureServices();

startup.ConfigureServices(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("devCorsPolicy");

app.Run();
