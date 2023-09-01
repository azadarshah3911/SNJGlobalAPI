using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog.Context;
using SNJGlobalAPI.DbModels.SNJContext;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.CommonRepos;
using SNJGlobalAPI.Repositories.Interfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using SNJGlobalAPI.Repositories.ProductionRepos;
using SNJGlobalAPI.Repositories.Repos;
using SNJGlobalAPI.SecurityHandlers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddResponseCompression((configureOption) =>
{
    configureOption.EnableForHttps = true;
});
*/
//Automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});

//controller
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

//Logs setting
builder.Logging.AddConsole().AddFile("Logs/snj-{Date}.txt");

builder.Services.AddCors(cors =>
{
    cors.AddPolicy("snjPolicy", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});


//Db Configuration
builder.Services.AddDbContext<GlobalAPIContext>(
    (contextLifetime) =>
    {
        contextLifetime.UseSqlServer(builder.Configuration.GetConnectionString("SnjCon"),
          sqlServerOptionsAction:
          (options) =>
          {
              options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
              //options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
              options.CommandTimeout(65);
          });
    });

#region JWT AUTHENTICATION SETTING
string key = builder.Configuration["JwtSettings:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateLifetime = true,
        ValidateAudience = false,
        // ValidAudiences = builder.Configuration.GetSection("JwtSettings:Audiences").Get<string[]>(),

    };
    x.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = async (context) =>
        {
            //context.Exception;
            //do action if any you want to do
        },
        OnChallenge = async (context) =>
        {
            context.HandleResponse();
            if (context.AuthenticateFailure is not null)
            {
                await context.Response.WriteAsync(context.Error);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token not attached");
            }
        }
    };
});
#endregion

#region Configuration services 
//paging conf
builder.Configuration.GetSection("Paging").Bind(new PagingInfo());
//email service config
builder.Configuration.GetSection("Smtp").Bind(new SmtpConfig());
#endregion

//Other Functionality based services
#region Common Services
//setting global exception handler
builder.Services.AddScoped<EHandler>();
//Jwt token handler custom class
builder.Services.AddTransient<IJwtHandler, JwtHandlerRepo>();

//General Db Service
builder.Services.AddScoped<IDb, DbRepo>();
builder.Services.AddScoped<ICommon, CommonRepo>();
#endregion
builder.Services.AddScoped<IAccount, AccountRepo>();
#region Production Services
builder.Services.AddScoped<IProduct, ProductRepo>();
builder.Services.AddScoped<IPatient, PatientRepo>();
builder.Services.AddScoped<ILead, LeadRepo>();
builder.Services.AddScoped<IProductQuestion, ProductQuestionRepo>();
builder.Services.AddScoped<IStatus, StatusRepo>();
builder.Services.AddScoped<IEligibility, EligibilityRepo>();
builder.Services.AddScoped<ILeadComment, LeadCommentRepo>();
builder.Services.AddScoped<ISns, SNSRepo>();
builder.Services.AddScoped<IQA, QARepo>();
builder.Services.AddScoped<ILeadAssigned, LeadAssignedRepo>();
builder.Services.AddScoped<IAgentBonus, AgentBonusRepo>();
builder.Services.AddScoped<IAgentHistory, AgentHistoryRepo>();
builder.Services.AddScoped<IProcessed, ProcessedRepo>();
builder.Services.AddScoped<IErrorLeads, ErrorLeadRepo>();
builder.Services.AddScoped<IChassingVerification, ChassingVerificationRepo>();
builder.Services.AddScoped<IConfirmation, ConfirmationRepo>();
builder.Services.AddScoped<IFullFill, FullFillRepo>();
#endregion
builder.Services.AddTransient<IDashboard, DashboardRepo>();
builder.Services.AddScoped<IDropDown, DropDownRepo>();
builder.Services.AddScoped<IUploadFile, UploadFileRepo>();

//For Getting Files In Chunks
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = int.MaxValue;
});

var app = builder.Build();
using (var service = app.Services.CreateScope())
{
    var db = service.ServiceProvider.GetRequiredService<GlobalAPIContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
        RequestPath = "/uploads"
    });
}

else if (!app.Environment.IsDevelopment())
{
    app.UseStaticFiles();

    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"C:\Web\GlobalAPI\Uploads")),
        RequestPath = new PathString("/Uploads")
    });
}
app.UseCors("snjPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<EHandler>();

app.MapControllers();

app.Run();

