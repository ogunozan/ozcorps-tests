var _builder = WebApplication.CreateBuilder(args);

_builder.Services.AddControllers();

_builder.Services.AddEndpointsApiExplorer();

_builder.Services.AddSwaggerGen();

_builder.Services.AddOzt();

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