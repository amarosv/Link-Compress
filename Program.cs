using link_compress_api.BL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();

// Configuraci�n de Swagger y su explorador de endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Aqu� agregamos el filtro de operaci�n para eliminar de Swagger las rutas que no queremos
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Link Compress API", Version = "v1" });
    options.EnableAnnotations(); // Habilitar anotaciones

});

var app = builder.Build();

// Configuraci�n de la solicitud HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Mapea la ruta personalizada pero sin exponerla en Swagger
app.MapGet("/{alias}", (string alias, HttpContext context) =>
{
    string url = clsMetodosURLBL.getLongUrlByAliasBL(alias);

    // Si la URL es v�lida y no es la misma que la actual
    if (!string.IsNullOrEmpty(url))
    {
        // Realiza la redirecci�n
        context.Response.Redirect(url, false);
    }
})
.ExcludeFromDescription();


app.Run("http://0.0.0.0:5000"); // Escucha en el puerto 5000
