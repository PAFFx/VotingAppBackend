using MongoDB.Driver;

// establish mongo connection
var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
if (connectionString == null)
{
    Console.WriteLine("MONGODB_URI env var not found");
    Environment.Exit(0);
}
var client = new MongoClient(connectionString);


// builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
