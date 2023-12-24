using DataAccess.BAL.DAOS.Implementations;
using DataAccess.BAL.DAOS.Interfaces;
using DataAccess.BAL.DTOS.AccountTypes;
using DataAccess.BAL.DTOS.Authentication;
using DataAccess.BAL.DTOS.BankAccounts;
using DataAccess.BAL.DTOS.MemberAccounts;
using DataAccess.BAL.Profiles;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT
#region JWT 
builder.Services.AddSwaggerGen(options =>
{
    // Set the comments path for the Swagger JSON and UI.

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bank Account Application API",
        Description = "JWT Authentication API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authentication",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Key"])),
            ValidateLifetime = true,//check access token het han
            ClockSkew = TimeSpan.Zero
        };
    });
#endregion

//ODATA
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<GetMemberAccount>("MemberAccounts");
modelBuilder.EntitySet<GetBankAccount>("BankAccounts");
modelBuilder.EntitySet<GetAccountType>("AccountTypes");

builder.Services.AddControllers().AddOData(option => option.Select()
                                                            .Filter()
                                                            .OrderBy()
                                                            .Expand()
                                                            .Count()
                                                            .SetMaxTop(null)
                                                            .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

//AddCors
builder.Services.AddCors(cors => cors.AddPolicy(
                                    name: "WebPolicy",
                                    builder =>
                                    {
                                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                    }
                               ));


//AddScope
builder.Services.Configure<JwtAuth>(builder.Configuration.GetSection("JwtAuth"));
builder.Services.AddScoped<IMemberAccountRepository,MemberAccountRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
builder.Services.AddScoped<IMemberAccountDAO,MemberAccountDAO>();
builder.Services.AddScoped<IBankAccountDAO, BankAccountDAO>();
builder.Services.AddScoped<IAccountTypeDAO, AccountTypeDAO>();

builder.Services.AddAutoMapper(typeof(AccountTypeProfile),
                                typeof(MemberAccountProfile),
                                typeof(BankAccountProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("WebPolicy");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
