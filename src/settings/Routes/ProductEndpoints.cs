using BugStore.Interfaces;
using BugStore.Models;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
    app.MapGet("/v1/products", async (IProductRepository repository) =>
   {
   var products = await repository.Get();
 return Results.Ok(products);
   });

   app.MapGet("/v1/products/{id}", async (Guid id, IProductRepository repository) =>
   {
   var product = await repository.GetById(id);
   if (product == null)
     return Results.NotFound();
   return Results.Ok(product);
    });

    app.MapPost("/v1/products", async (Product product, IProductRepository repository) =>
   {
   if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
 return Results.BadRequest("Dados obrigatórios ausentes ou inválidos.");

  product.Id = Guid.NewGuid();
 await repository.Create(product);
      return Results.Created($"/v1/products/{product.Id}", product);
  });

  app.MapPut("/v1/products/{id}", async (Guid id, Product product, IProductRepository repository) =>
    {
        var existing = await repository.GetById(id);
  if (existing == null)
       return Results.NotFound();

   existing.Name = product.Name;
   existing.Price = product.Price;
   // Adicione outros campos conforme necessário

   var updated = await repository.Update(existing);
   if (!updated)
       return Results.BadRequest("Não foi possível atualizar o produto.");

        return Results.Ok(existing);
  });

  app.MapDelete("/v1/products/{id}", async (Guid id, IProductRepository repository) =>
    {
        var product = await repository.GetById(id);
  if (product == null)
       return Results.NotFound();

   await repository.Delete(product);
   return Results.NoContent();
        });
    }
}
