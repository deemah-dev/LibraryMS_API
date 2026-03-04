using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration["JWT_SECRET_KEY"];

if (string.IsNullOrWhiteSpace(secretKey))
{
    throw new Exception("JWT secret key is not configured.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer = "StudentApi",

            ValidAudience = "StudentApiUsers",

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey))
        };
    });



builder.Services.AddAuthorization();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("AuthLimiter", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            });
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",


        Type = SecuritySchemeType.Http,


        Scheme = "Bearer",


        BearerFormat = "JWT",


        In = ParameterLocation.Header,


        Description = "Enter: Bearer {your JWT token}"
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


            new string[] {}
        }
    });
});

//builder.Services.AddScoped<IStudentService, StudentService>();
//builder.Services.AddScoped<IStudentsRepo, StudentsRepo>();

//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IUserRepo, UserRepo>();

//builder.Services.AddScoped<IRoleService, RoleService>();
//builder.Services.AddScoped<IRoleRepo, RoleRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("StudentApiCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7224",
                "http://localhost:5057"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
    {
        await context.Response.WriteAsync("Too many login attempts. Please try again later.");
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
