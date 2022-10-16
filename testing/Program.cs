using testing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DocStrings DocStrings = new DocStrings();

int numeroAMultiplicar = 2;


// coloca el mouse encima de la funcion "mult"
Console.WriteLine(DocStrings.mult(numeroAMultiplicar));

app.Run();


