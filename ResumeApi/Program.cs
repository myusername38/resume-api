

using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ResumeApi;
using ResumeApi.Repos;
using ResumeApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using ResumeApi.Auth;
using Microsoft.Extensions.Configuration;
using ResumeApi.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(IStartup));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


// repos
builder.Services.AddTransient<UserRepo>();
builder.Services.AddTransient<SolutionRepo>();
builder.Services.AddTransient<UserSolutionRepo>();

// services

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISolutionService, SolutionService>();
builder.Services.AddTransient<ILikeService, LikeService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IEmailTemplateService, EmailTemplateService>();

// Authorization
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        policy => policy.Requirements.Add(new RoleRequirement(AuthRole.Owner)));
    options.AddPolicy("Follower",
        policy => policy.Requirements.Add(new RoleRequirement(AuthRole.Follower)));
});

builder.Services.Configure<MailSettings>(options => builder.Configuration.GetSection("MailSettings").Bind(options));

builder.Services.AddControllers();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigin", builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});


var app = builder.Build();
app.UseCors("AnyOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();

