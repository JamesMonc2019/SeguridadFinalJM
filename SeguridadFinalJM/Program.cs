using Microsoft.EntityFrameworkCore;
using SeguridadFinalJM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

var connecctionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(connecctionString);
});


builder.Services.AddCors(options =>
{
    var frontend = builder.Configuration.GetValue<string>("frontend");
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(frontend).AllowAnyMethod().AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
