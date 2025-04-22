using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var pluginPath = @"C:\Users\Ali Can\source\repos\PluginControllerApi\WebApplication1\bin\Debug\net9.0\WebApplication1.dll";


if (File.Exists(pluginPath))
{
    var pluginAssembly = Assembly.LoadFrom(pluginPath);
    builder.Services.AddMvc()
        .PartManager.ApplicationParts.Add(new AssemblyPart(pluginAssembly));
}
else
{
    Console.WriteLine("Plugin DLL not found.");
}

var app = builder.Build();
app.UseCors("AllowAll"); 
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plugin API V1");
});

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
