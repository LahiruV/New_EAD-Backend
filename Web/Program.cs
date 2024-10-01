using Web.DataAccessLayer.Services;
using Web.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICrudDL, CrudDL>();
builder.Services.AddScoped<IUserDL, UserDL>();
builder.Services.AddScoped<ITravellerDL, TravellerDL>();
builder.Services.AddScoped<ITicketDL, TicketDL>();
builder.Services.AddScoped<ITrainDL, TrainDL>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
