using BugStore.Interfaces;
using BugStore.Models;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/v1/orders/{id}", async (Guid id, IOrderRepository repository) =>
        {
            var order = await repository.GetById(id);
            if (order == null)
                return Results.NotFound();
            return Results.Ok(order);
        });

        app.MapPost("/v1/orders", async (Order order, IOrderRepository repository) =>
        {
            if (order.CustomerId == Guid.Empty || order.Lines == null || !order.Lines.Any())
                return Results.BadRequest("Dados obrigat�rios ausentes ou inv�lidos.");

            order.Id = Guid.NewGuid();
            await repository.Create(order);
            return Results.Created($"/v1/orders/{order.Id}", order);
        });

        app.MapPut("/v1/orders/{id}", async (Guid id, Order order, IOrderRepository repository) =>
   {
       var existing = await repository.GetById(id);
       if (existing == null)
           return Results.NotFound();

       existing.CustomerId = order.CustomerId;
       existing.Lines = order.Lines;
       // Adicione outros campos conforme necess�rio

       var updated = await repository.Update(existing);
       if (!updated)
           return Results.BadRequest("N�o foi poss�vel atualizar o pedido.");

       return Results.Ok(existing);
   });

        app.MapDelete("/v1/orders/{id}", async (Guid id, IOrderRepository repository) =>
    {
        var order = await repository.GetById(id);
        if (order == null)
            return Results.NotFound();

        await repository.Delete(order);
        return Results.NoContent();
    });
    }
}
