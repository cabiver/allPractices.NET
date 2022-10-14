

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

app.UseHttpMethodOverride();

app.UseRouting();

app.Use(async (context, next) =>
{
    // TODO: entender mas a profundidad la authorizacion del backend
    Console.WriteLine(context);
    Console.WriteLine(context.GetEndpoint());
    Console.WriteLine(context.GetEndpoint()?.Metadata.GetMetadata<RequiresAuditAttribute>());

    if (context.GetEndpoint()?.Metadata.GetMetadata<RequiresAuditAttribute>() is not null)
    {
        Console.WriteLine($"ACCESS TO SENSITIVE DATA AT: {DateTime.UtcNow}");
    }

    await next(context);
});

app.MapGet("/", () => "Audit isn't required.");
app.MapGet("/sensitive", () => "Audit required for sensitive data.")
    .WithMetadata(new RequiresAuditAttribute());

app.MapControllers();

app.Run();



public class RequiresAuditAttribute : Attribute { }