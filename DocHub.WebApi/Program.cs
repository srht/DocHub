using DocHub.Data;
using DocHub.Data.Abstracts;
using DocHub.Data.Repositories;
using DocHub.Service;
using DocHub.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((ctx,lc)=>lc
                        .WriteTo.Console()
                        .ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddLogging(loggingBuilder=>
                            loggingBuilder.AddSerilog(dispose:true));
builder.Services.AddDbContext<DocHubDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DocHubConnectionString")));
builder.Services.AddScoped<IDocumentRepository,DocumentRepository>();
builder.Services.AddScoped<IDocumentsService, DocumentService>();
builder.Services.AddSingleton<IFileStoreService>(fileService=>new FileStoreService(Directory.GetCurrentDirectory()));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          //policy.WithOrigins("http://localhost:4200");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                      });
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("MyAllowSpecificOrigins");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
