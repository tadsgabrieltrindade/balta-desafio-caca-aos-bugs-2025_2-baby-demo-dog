using BugStore.Interfaces;
using BugStore.Models;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/v1/customers", async (ICustomerRepository repository) =>
        {
            var customers = await repository.Get();
            return Results.Ok(customers);
        }); 

        app.MapGet("/v1/customers/{id}", async (Guid id, ICustomerRepository repository) =>
        {
            var customer = await repository.GetById(id);
            if (customer == null)
                return Results.NotFound();
            return Results.Ok(customer);
        });
        
        app.MapPost("/v1/customers", async (Customer customer, ICustomerRepository repository) =>
        {
            if (string.IsNullOrWhiteSpace(customer.Name) ||
                string.IsNullOrWhiteSpace(customer.Email) ||
                string.IsNullOrWhiteSpace(customer.Phone) ||
                customer.BirthDate == default)
            {
                return Results.BadRequest("Dados obrigatórios ausentes ou inválidos.");
            }

            customer.Id = Guid.NewGuid();
            await repository.Create(customer);
            return Results.Created($"/v1/customers/{customer.Id}", customer);
        });

        app.MapPut("/v1/customers/{id}", async (Guid id, Customer customer, ICustomerRepository repository) =>
        {
            var existing = await repository.GetById(id);
            if (existing == null)
                return Results.NotFound();

            // Atualiza os campos
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.BirthDate = customer.BirthDate;

            var updated = await repository.Update(existing);
            if (!updated)
                return Results.BadRequest("Não foi possível atualizar o cliente.");

            return Results.Ok(existing);
        });

        app.MapDelete("/v1/customers/{id}", async (Guid id, ICustomerRepository repository) =>
        {
            var customer = await repository.GetById(id);
            if (customer == null)
                return Results.NotFound();

            await repository.Delete(customer);
            return Results.NoContent();
        });
    }
}