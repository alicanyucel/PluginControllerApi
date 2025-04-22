using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", "Hello", "bin", "Debug", "net9.0");
var pluginDll = Path.Combine(pluginPath, "Hello.dll");

if (File.Exists(pluginDll))
{
    var pluginAssembly = Assembly.LoadFrom(pluginDll);

    // Plugin assembly'lerini uygulama kýsmýna ekle
    builder.Services.AddControllers()
                    .PartManager.ApplicationParts.Add(new AssemblyPart(pluginAssembly));
}
else
{
    Console.WriteLine("Plugin DLL not found.");
}

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plugin API V1");
});
app.UseRouting();
app.MapControllers();

app.Run();
