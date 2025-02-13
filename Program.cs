using link_compress_api.BL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Link Compress API", Version = "v1" });
    options.EnableAnnotations();
});

// Habilitar CORS para TODOS los orígenes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  // Permite cualquier origen
                  .AllowAnyMethod()  // Permite cualquier método (GET, POST, PUT, DELETE, etc.)
                  .AllowAnyHeader(); // Permite cualquier encabezado
        });
});

var app = builder.Build();

// Habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Habilitar HTTPS
app.UseHttpsRedirection();
app.UseAuthorization();

// Habilitar archivos estáticos
app.UseDefaultFiles(); // Habilita index.html automáticamente
app.UseStaticFiles();  // Permite servir archivos de wwwroot

app.MapControllers();

// Ruta personalizada
app.MapGet("/{alias}", (string alias, HttpContext context) =>
{
    var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
    if (!string.IsNullOrEmpty(ip)) ip = ip.Split(',')[0].Trim();
    if (string.IsNullOrEmpty(ip)) ip = context.Connection.RemoteIpAddress?.ToString();
    if (string.IsNullOrEmpty(ip)) ip = "No se pudo obtener la IP del usuario.";

    string url = clsMetodosURLBL.getLongUrlByAliasBL(alias, ip);
    if (!string.IsNullOrEmpty(url))
    {
        context.Response.Redirect(url, false);
    }
})
.ExcludeFromDescription();

// Escuchar en el puerto 5000
app.Run("http://0.0.0.0:5000");

