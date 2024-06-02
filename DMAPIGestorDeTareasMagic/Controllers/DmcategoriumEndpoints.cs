using Microsoft.EntityFrameworkCore;
using DMAPIGestorDeTareasMagic.Data;
using DMAPIGestorDeTareasMagic.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace DMAPIGestorDeTareasMagic.Controllers;

public static class DmcategoriumEndpoints
{
    public static void MapDmcategoriumEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Dmcategorium").WithTags(nameof(Dmcategorium));

        group.MapGet("/", async (DMAPIGestorDeTareasMagicContext db) =>
        {
            return await db.Dmcategorium.ToListAsync();
        })
        .WithName("GetAllDmcategoria")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Dmcategorium>, NotFound>> (int dmcategoriaid, DMAPIGestorDeTareasMagicContext db) =>
        {
            return await db.Dmcategorium.AsNoTracking()
                .FirstOrDefaultAsync(model => model.DmcategoriaId == dmcategoriaid)
                is Dmcategorium model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDmcategoriumById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int dmcategoriaid, Dmcategorium dmcategorium, DMAPIGestorDeTareasMagicContext db) =>
        {
            var affected = await db.Dmcategorium
                .Where(model => model.DmcategoriaId == dmcategoriaid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.DmcategoriaId, dmcategorium.DmcategoriaId)
                    .SetProperty(m => m.Dmnombre, dmcategorium.Dmnombre)
                    .SetProperty(m => m.Dmdescripcion, dmcategorium.Dmdescripcion)
                    .SetProperty(m => m.DmtareaId, dmcategorium.DmtareaId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDmcategorium")
        .WithOpenApi();

        group.MapPost("/", async (Dmcategorium dmcategorium, DMAPIGestorDeTareasMagicContext db) =>
        {
            db.Dmcategorium.Add(dmcategorium);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Dmcategorium/{dmcategorium.DmcategoriaId}",dmcategorium);
        })
        .WithName("CreateDmcategorium")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int dmcategoriaid, DMAPIGestorDeTareasMagicContext db) =>
        {
            var affected = await db.Dmcategorium
                .Where(model => model.DmcategoriaId == dmcategoriaid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDmcategorium")
        .WithOpenApi();
    }
}
