using Microsoft.EntityFrameworkCore;
using Ozcorps.Generic.ApiTest.Bll;
using Ozcorps.Generic.ApiTest.Dal;
using Ozcorps.Generic.Controllers;

var _builder = WebApplication.CreateBuilder(args);

_builder.Services.AddControllers();

_builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new GeometryJsonConverter());
});

_builder.Services.AddDbContext<DbContext, BaseDbContext>(options =>
    options.UseNpgsql(_builder.Configuration.GetConnectionString("Postgre"),
        x => x.UseNetTopologySuite()));

_builder.Services.AddGeneric();

_builder.Services.AddScoped<IPoiService, PoiService>();
        
_builder.Services.AddEndpointsApiExplorer();

_builder.Services.AddSwaggerGen();

_builder.Services.AddOzTextLogger();

var _app = _builder.Build();

if (_app.Environment.IsDevelopment())
{
    _app.UseSwagger();

    _app.UseSwaggerUI();
}

_app.UseHttpsRedirection();

_app.UseAuthorization();

_app.MapControllers();

_app.Run();
