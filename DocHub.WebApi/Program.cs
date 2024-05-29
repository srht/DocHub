using DocHub.Core.Entities.Users;
using DocHub.Data;
using DocHub.Data.Abstracts;
using DocHub.Data.Repositories;
using DocHub.Service;
using DocHub.Service.Abstracts;
using DocHub.Service.Abstracts.Users;
using DocHub.Service.Mappers;
using DocHub.Service.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((ctx,lc)=>lc
                        .WriteTo.Console()
                        .ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddLogging(loggingBuilder=>
                            loggingBuilder.AddSerilog(dispose:true));
builder.Services.AddDbContext<DocHubDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DocHubConnectionString")));
builder.Services.AddDbContext<AuthorizationDbContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DocHubConnectionString")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthorizationDbContext>();
// These will eventually be moved to a secrets file, but for alpha development appsettings is fine
var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ),
        };
    });
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocumentRepository,DocumentRepository>();
builder.Services.AddScoped<IDocumentsService, DocumentService>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(a=>a.AddProfile<CategoryMapper>());
builder.Services.AddAutoMapper(a=>a.AddProfile<DocumentMapper>());
builder.Services.AddSingleton<IFileStoreService>(fileService=>new FileStoreService(Directory.GetCurrentDirectory()));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          //policy.WithOrigins("http://localhost:4200"); // ---> cookie token
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                          //policy.AllowCredentials(); --> cookie token
                      });
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles;
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("MyAllowSpecificOrigins");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
