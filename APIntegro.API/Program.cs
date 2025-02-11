using APIntegro.Application;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.Authentication;
using APIntegro.Application.Services.Communication;
using APIntegro.Application.Services.WorkFlows;
using APIntegro.Domain.Entities;
using APIntegro.Infrastructure;
 


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");   
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();



static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddInfrastructureServices();
    builder.Services.AddApplicationServices();
    builder.Services.AddSingleton<LoginSession>();
    //builder.Services.AddHostedService<ProjectTaskBackgroundService>();
    builder.Services.AddHostedService<ProjectWorkFlow>();
    builder.Services.Configure<SMSSettings>(builder.Configuration.GetSection("SMSSettings"));
    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("MailSettings"));
    builder.Services.Configure<TrelloSettings>(builder.Configuration.GetSection("TrelloSettings"));
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}