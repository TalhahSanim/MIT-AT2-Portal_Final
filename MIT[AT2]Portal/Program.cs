using Microsoft.EntityFrameworkCore;
using MIT_AT2_Portal.Models;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder()
                                         .AddJsonFile("./config.json")
                                         .Build();
                                     
builder.Services.AddMvc();

builder.Services.AddDbContext<StudentDataContext>(options =>
{
    var connectionString = configuration.GetConnectionString("DBConnection");
    options.UseSqlServer(connectionString);
});





var app = builder.Build();
bool isDevelopment = configuration.GetValue<bool>("EnableDeveloperExceptions");
if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error.html");
}

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("/invalid"))
        throw new Exception();
    await next();
});




app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=display}/{id?}");


app.UseFileServer();
app.Run();
