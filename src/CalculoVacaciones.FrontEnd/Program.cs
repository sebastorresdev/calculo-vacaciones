using CalculoVacaciones.Data.Interfaces;
using CalculoVacaciones.Data.Repositories;
using CalculoVacaciones.Negocios.Interfaces;
using CalculoVacaciones.Negocios.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("CadenSQL") ?? throw new NullReferenceException("No se pudo obtener la cadena de conexion");

// Dependencias de los repositorios
builder.Services.AddScoped<IEmpleadoRepository>(provider => new EmpleadoRepository(connectionString));
builder.Services.AddScoped<IDepartamentoRepository>(provider => new DepartamentoRepository(connectionString));
builder.Services.AddScoped<ITipoEmpleadoRepository>(provider => new TipoEmpleadoRepository(connectionString));

// Dependencias de los servicios
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<ITipoEmpleadoService, TipoEmpleadoService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
