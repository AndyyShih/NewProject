using System.Reflection;
using DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region SwaggerUI

builder.Services.AddSwaggerGen(c =>
{
    // ���J�D�M�ת� XML �����ɮ�
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    //���J��L���O�w�� XML �����ɮ�
    var businessRuleXml = Path.Combine(AppContext.BaseDirectory,"BusinessRule.xml");
    var dataAccessXml = Path.Combine(AppContext.BaseDirectory,"DataAccess.xml");
    var commonXml = Path.Combine(AppContext.BaseDirectory,"Common.xml");

    c.IncludeXmlComments(businessRuleXml);
    c.IncludeXmlComments(dataAccessXml);
    c.IncludeXmlComments(commonXml);

});

//�����y�z
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NewProject V1");
    });
}

#endregion

#region Database

builder.Services.AddDbContext<ProjectContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("NewProject_Db"))
);

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
