using NoteApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Application Katmaný Baðýmlýlýklarý (MediatR)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(NoteApp.Application.Common.Interfaces.IApplicationDbContext).Assembly));

// 2. Infrastructure Katmaný Baðýmlýlýklarý (EF Core & SQL Server)
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. Statik Dosya Desteði (Ön yüz için)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();