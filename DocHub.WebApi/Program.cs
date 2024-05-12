using DocHub.Data;
using DocHub.Data.Abstracts;
using DocHub.Data.Repositories;
using DocHub.Service;
using DocHub.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
