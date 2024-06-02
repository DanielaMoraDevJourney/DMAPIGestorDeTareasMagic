using Microsoft.EntityFrameworkCore;
using DMAPIGestorDeTareasMagic.Data;
using DMAPIGestorDeTareasMagic.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace DMAPIGestorDeTareasMagic.Controllers;

public static class DmprioridadEndpoints
{
    public static void MapDmprioridadEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Dmprioridad").WithTags(nameof(Dmprioridad));

        group.MapGet("/", async (DMAPIGestorDeTareasMagicContext db) =>
        {
            return await db.Dmprioridad.ToListAsync();
        })
        .WithName("GetAllDmprioridads")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Dmprioridad>, NotFound>> (int dmprioridadid, DMAPIGestorDeTareasMagicContext db) =>
        {
            return await db.Dmprioridad.AsNoTracking()
                .FirstOrDefaultAsync(model => model.DmprioridadId == dmprioridadid)
                is Dmprioridad model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDmprioridadById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int dmprioridadid, Dmprioridad dmprioridad, DMAPIGestorDeTareasMagicContext db) =>
        {
            var affected = await db.Dmprioridad
                .Where(model => model.DmprioridadId == dmprioridadid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.DmprioridadId, dmprioridad.DmprioridadId)
                    .SetProperty(m => m.Dmnombre, dmprioridad.Dmnombre)
                    .SetProperty(m => m.Dmdescripcion, dmprioridad.Dmdescripcion)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDmprioridad")
        .WithOpenApi();

        group.MapPost("/", async (Dmprioridad dmprioridad, DMAPIGestorDeTareasMagicContext db) =>
        {
            db.Dmprioridad.Add(dmprioridad);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Dmprioridad/{dmprioridad.DmprioridadId}",dmprioridad);
        })
        .WithName("CreateDmprioridad")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int dmprioridadid, DMAPIGestorDeTareasMagicContext db) =>
        {
            var affected = await db.Dmprioridad
                .Where(model => model.DmprioridadId == dmprioridadid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDmprioridad")
        .WithOpenApi();
    }
}
