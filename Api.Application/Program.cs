using AutoMapper;
using CrossCutting.DependencyInjection;
using CrossCutting.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);
    serverOptions.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps());
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

ConfigureRepository.ConfigureDependenciesRepository(builder.Services);
ConfigureService.ConfigureDependenciesService(builder.Services);

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new EntityToDTOProfile());
});

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }