using Microsoft.EntityFrameworkCore;
using TMS.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura o DbContext para usar a string de conex�o do appsettings.json
builder.Services.AddDbContext<BackEndContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configura o pipeline de requisi��o HTTP.
/*/if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NomeDoSeuProjeto v1"));
}*/

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NomeDoSeuProjeto v1"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();