using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sale.application.Commands.CreateSale;
using sale.application.Common;
using sale.application.Producers;
using sale.application.Queries.GetSale;
using sale.infrastructure.Data;
using sale.infrastructure.Producers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("SaleDb"));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
}

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSaleCommandHandler).Assembly));

builder.Services.AddScoped<IEventProducer, EventProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(appDbContext);
    }    
}

app.UseHttpsRedirection();

app.MapGet("/sale", async (IMediator mediator, [AsParameters] GetSaleQuery query) =>
{
    var sale = await mediator.Send(query);

    if (sale is null)
        return Results.NotFound();

    return Results.Ok(sale);
})
.WithName("GetSale")
.WithOpenApi();

app.MapPost("/sale", async (IMediator mediator, CreateSaleCommand command) =>
{
    return await mediator.Send(command);
})
.WithName("CreateSale")
.WithOpenApi();

app.Run();

