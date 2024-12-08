using CalculoVacaciones.Negocios.Interfaces;
using CalculoVacaciones.Negocios.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("CadenSQL") ?? throw new NullReferenceException("No se pudo obtener la cadena de conexion");

// Dependencias de los servicios
builder.Services.AddScoped<IEmpleadoService>(provider => new EmpleadoService(connectionString));
builder.Services.AddScoped<IDepartamentoService>(provider => new DepartamentoService(connectionString));
builder.Services.AddScoped<ITipoEmpleadoService>(provider => new TipoEmpleadoService(connectionString));


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
