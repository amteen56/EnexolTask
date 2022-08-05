using EnexolTask.Application;
using MediatR;
using System.Reflection;
using EnexolTask.Persistence;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EnexolTask.Application.Wrappers;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
           {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = false,//if False then that the expired token is considered valid
               ValidateIssuerSigningKey = true,
               RequireExpirationTime = false,
               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Issuer"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))//key must be min 16 chars
           };
           options.Events = new JwtBearerEvents
           {
               OnAuthenticationFailed = c =>
               {
                   if (c.Exception.GetType() == typeof(SecurityTokenExpiredException))
                   {
                       c.Response.StatusCode = 401;
                       c.Response.ContentType = "application/json";
                       var result = JsonConvert.SerializeObject(new Response<string>("Token Expired"));
                       return c.Response.WriteAsync(result);
                   }
                   else
                   {
                       return Task.CompletedTask;
                   }
               },
           };
           });
// Add services to the container.
builder.Services.ConfigureApplicationService();
builder.Services.ConfigurePersistenceService(builder.Configuration);

builder.Services.AddControllers();
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {

        Version = "v2",
        Title = "AgencyCare API",
        Description = "AgencyCare API-v1",
        TermsOfService = new Uri("https://www.adamjeelife.com"),
        Contact = new OpenApiContact
        {
            Name = "AdamjeeLife",
            Email = string.Empty,
            Url = new Uri("https://www.adamjeelife.com"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://www.adamjeelife.com/"),
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
    c.ResolveConflictingActions(apiDesc => apiDesc.First());
});
           var app = builder.Build();

           // Configure the HTTP request pipeline.
           if (app.Environment.IsDevelopment())
           {
               app.UseSwagger();
               app.UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("v2/swagger.json", "AgencyCare APIv1");
               });
           }

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

           app.UseAuthentication();
           app.UseAuthorization();
           app.MapControllers();

app.Run();
