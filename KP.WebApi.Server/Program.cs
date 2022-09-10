using KP.Core.DomainModels;
using KP.Web.Api;


using Microsoft.EntityFrameworkCore;
using KP.Core.Data;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json", optional: false)
       .Build();
// Add services to the container.

builder.Services.ConfigureApplicationServices();

builder.Services.AddDbContext<PharmacyContext>(options =>
    options.UseSqlServer(
        config.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext <IDbContext, PharmacyContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
