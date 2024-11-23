using AwesomeGIC.Bank.Application;
using AwesomeGIC.Bank.Application.Common;
using AwesomeGIC.Bank.Infrastructure.Sql;
using AwesomeGIC.Bank.Web.Api.BackgroundTask;
using AwesomeGIC.Bank.Web.Api.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateSlimBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
//    //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
//    //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
//});
builder.Services.AddApplicationServices();

builder.Services.AddDbContext<AwesomeGICBankDBContext>(o =>
{
    //o.UseInMemoryDatabase("Employee");
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAwesomeGICBankDBContext, AwesomeGICBankDBContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(o =>
{
    o.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(12);
        options.PermitLimit = 4;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    });
});

builder.Services.Configure<HostOptions>(o =>
{ 
    o.ServicesStartConcurrently = true;
    o.ServicesStopConcurrently = true;
});

builder.Services.AddHostedService<PeriodicBackgroundTask>();

builder.Services.AddHealthChecks().AddCheck<CustomHealthCheck>("custom_health_check");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
});

app.Run();
