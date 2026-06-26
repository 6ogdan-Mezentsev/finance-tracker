using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using report_service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secret = jwtSettings["Secret"] ?? "YourSuperSecretKeyHereWhichIsAtLeast32BytesLong!";
var issuer = jwtSettings["Issuer"] ?? "auth-service";
var audience = jwtSettings["Audience"] ?? "finance-tracker";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

builder.Services.AddScoped<IReportEngine, ReportEngine>();

builder.Services.AddHttpClient<IFinanceServiceClient, FinanceServiceClient>(client =>
{
    var financeServiceUrl = builder.Configuration["FinanceService:BaseUrl"] ?? "http://finance-service:8080";
    client.BaseAddress = new Uri(financeServiceUrl);
});

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
