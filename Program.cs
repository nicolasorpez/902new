using IDGS902UT.API.DbContexts;
using IDGS902UT.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
  .AddXmlDataContractSerializerFormatters();//permite habilitar el modelo mvc

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//registrar mi repositorio

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

//registrar auto mapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//registrar el contexto de datos

builder.Services.AddDbContext<CityInfoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hola Mundo");
//});

app.Run();