using System.Reflection;
using BusinessRule.Middleware;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewProject.DependencyInjectionTool;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//引用DI註冊方法
DependencyInjectionTool.AddDIContainer(builder.Services);

#region Cors Setting

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigin", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

#endregion

#region SwaggerUI

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NewProject",
        Version = "2024.10.11",
        Description = "空專案範本",
    });

    // 載入主專案的 XML 註解檔案
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    //載入其他類別庫的 XML 註解檔案
    var businessRuleXml = Path.Combine(AppContext.BaseDirectory,"BusinessRule.xml");
    var dataAccessXml = Path.Combine(AppContext.BaseDirectory,"DataAccess.xml");
    var commonXml = Path.Combine(AppContext.BaseDirectory,"Common.xml");

    c.IncludeXmlComments(businessRuleXml);
    c.IncludeXmlComments(dataAccessXml);
    c.IncludeXmlComments(commonXml);

});

#endregion

#region Serilog

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
    path: "logs/log-.txt",
    rollingInterval: RollingInterval.Day,
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

// 註冊全局異常處理中間件
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();
app.Run();
