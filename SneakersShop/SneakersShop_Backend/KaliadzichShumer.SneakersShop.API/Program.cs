using System.Reflection;
using KaliadzichShumer.SneakersShop.INTERFACES;
using KaliadzichShumer.SneakersShop.INTERFACES.Config;
using KaliadzichShumer.SneakersShop.BLC.Services;
using KaliadzichShumer.SneakersShop.INTERFACES.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var activeProfile = builder.Configuration.GetValue<string>("DAOConfig:ActiveProfile");
var daoConfig = builder.Configuration.GetSection($"DAOConfig:{activeProfile}").Get<DAOConfig>();

if (daoConfig == null){
  throw new Exception($"DAO configuration for profile '{activeProfile}' not found");
}
  

var daoPath = Path.Combine(AppContext.BaseDirectory, daoConfig.AssemblyName);
if (!File.Exists(daoPath)){
    throw new FileNotFoundException($"DAO assembly not found at path: {daoPath}");
}

var assembly = Assembly.LoadFrom(daoPath);
var daoType = assembly.GetType(daoConfig.TypeName);

if (daoType == null){
throw new Exception($"Type {daoConfig.TypeName} not found in assembly {daoConfig.AssemblyName}");
}
    

builder.Services.AddScoped<IDAO>(sp => 
{
    if (activeProfile == "Mock"){

        return (IDAO)Activator.CreateInstance(daoType);
    }
    else{
        return (IDAO)Activator.CreateInstance(daoType, daoConfig.ConnectionString);
    }
       
});

builder.Services.AddScoped<ISneakersShopService, SneakersShopService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run(); 