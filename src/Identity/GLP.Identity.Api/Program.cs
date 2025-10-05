using GLP.Identity.Api;
using GLP.Identity.Infrastructure;
using GLP.Identity.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityEndpoints();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

// Seed InMemory (solo para demo)
await Seed.InMemoryAsync(app.Services);

await app.RunAsync();