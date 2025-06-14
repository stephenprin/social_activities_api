using API.Middleware;
using Application.Activities.Queries;
using Application.Activities.Validators;
using Application.Core;
using Application.Interface;
using Domain;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddControllers(opt=>{
    var policy= new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddMediatR(x=> { 
    x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>();
    x.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
builder.Services.AddTransient<ExceptionMiddle>();
builder.Services.AddIdentityApiEndpoints<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedAccount = false;
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 6;
}).AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<AppDbContext>();
 

var app = builder.Build();
app.UseMiddleware<ExceptionMiddle>();

app.UseCors(policy => policy.AllowAnyHeader().AllowCredentials()
.AllowAnyMethod().WithOrigins("http://localhost:3000"));


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapGroup("api").MapIdentityApi<User>();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}



// Configure the HTTP request pipeline.


app.Run();
