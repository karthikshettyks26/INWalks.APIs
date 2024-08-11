using INWalks.API.Data;
using INWalks.APIs.Data;
using INWalks.APIs.Mappings;
using INWalks.APIs.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog;
using INWalks.APIs.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Add Logging configuration using serilog.
var logger = new LoggerConfiguration()
    .WriteTo.Console().WriteTo.File("Logs/INWalks_Log.txt",rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information().
    CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//To make Swagger accept bearer.
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "INWalks API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id =JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//DB Connection
builder.Services.AddDbContext<INWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("INWalksConnectionString")));

//Auth Db Connection
builder.Services.AddDbContext<INWalksAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("InWalksAuthConnectionString")));

// Add services to the container.
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository,TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

//Identity
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("INwalks")
    .AddEntityFrameworkStores<INWalksAuthDbContext>()
    .AddDefaultTokenProviders();

//settings for password.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//Jwt Authetication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//custom middleware for exception handler
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

//Authentication
app.UseAuthentication();

app.UseAuthorization();

//To make application serves static files like image.
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
