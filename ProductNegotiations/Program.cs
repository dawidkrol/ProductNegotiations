using FirebaseAdmin;
using FluentValidation;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ProductNegotiations.API.Models;
using ProductNegotiations.API.Validators;
using ProductNegotiations.Database.Library;
using ProductNegotiations.Database.Library.Services;
using ProductNegotiations.Library.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE_CONFIG"))
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetValue<string>("firebaseAuthorityUrl");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetValue<string>("firebaseAuthorityUrl"),
            ValidateAudience = true,
            ValidAudience = builder.Configuration.GetValue<string>("firebaseAppId"),
            ValidateLifetime = true
        };
    });

builder.Services.AddDbContext<NegotiationDbContext>();

// Database services
builder.Services.AddTransient<INegotiationDBService, NegotiationDBService>();
builder.Services.AddTransient<IProductDBService, ProductDBService>();

// Services
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<INegotiaitionService, NegotiaitionService>();

// Validators
builder.Services.AddTransient<IValidator<NegotiationClientCreateModel>, NegotiationClientValidator>();
builder.Services.AddTransient<IValidator<ProductClientModel>, ProductValidation>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductNegotiations.API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
