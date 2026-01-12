using DigitalOOH.API.DataAccess.DBContext;
using DigitalOOH.API.Middlewares;
using DigitalOOH.API.Models.Shared.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

/**
 * ===============================
 *      Database connection
 * ===============================
 */
string CONNECTION_STRING = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
builder.Services.AddDbContext<DigitalOOHDbContext>(options => options.UseSqlServer(CONNECTION_STRING));


/**
 * ===============================
 *      Configure Cors Policy
 * ===============================
 */
 
string[] AllowedOrigins = (builder.Configuration["AppSettings:AllowedOrigins"] ?? "").Split(","); // allowed array of origins from configuration 
builder.Services.AddCors(options =>
{
options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
                .AllowAnyMethod();

        if (AllowedOrigins.Length.Equals(0)) return;

        if(AllowedOrigins.Contains("*"))
        {
            builder.SetIsOriginAllowed(option => true);
        }
        else 
        {
            builder.WithOrigins(AllowedOrigins)
                    .AllowCredentials();    // FOR: Future if we want to allow cookies or authentication headers
        }
    });
});

/**
 * ===============================
 *      JWT Configuration
 * ===============================
 */
JwtConfig JWT_CONFIG = builder.Configuration.GetSection("Jwt").Get<JwtConfig>() ?? new JwtConfig();
builder.Services.AddSingleton<JwtConfig>(JWT_CONFIG);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // FOR: Production should be true
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = JWT_CONFIG.Issuer,
        ValidAudience = JWT_CONFIG.Audience,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_CONFIG.SecretKey)),
        ClockSkew = TimeSpan.FromMinutes(JWT_CONFIG.AccessTokenClockSkewMin)
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.OnStarting(async () =>
            {
                context.NoResult();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Append("Token-Expired", "true");
                await context.Response.WriteAsync(context.Exception.Message);
            });
            return Task.CompletedTask;
        }
    };
});

builder.Services
    .AddMemoryCache()
    .AddCoreServices()
    .AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ===============================
//      Swagger Configuration
// ===============================

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Digital OOH API",
        Description = "Digital Out-Of-Home Advertising Platform API"
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        BearerFormat = "JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
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

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
