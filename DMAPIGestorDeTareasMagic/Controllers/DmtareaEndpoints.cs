using Microsoft.EntityFrameworkCore;
using DMAPIGestorDeTareasMagic.Data;
using DMAPIGestorDeTareasMagic.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace DMAPIGestorDeTareasMagic.Controllers;

public static class DmtareaEndpoints
{
    public static void MapDmtareaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Dmtarea").WithTags(nameof(Dmtarea));

        group.MapGet("/", async (DMAPIGestorDeTareasMagicContext db) =>
        {
            return await db.Dmtarea.ToListAsync();
        })
        .WithName("GetAllDmtareas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Dmtarea>, NotFound>> (int dmtareaid, DMAPIGestorDeTareasMagicContext db) =>
        {
            return await db.Dmtarea.AsNoTracking()
                .FirstOrDefaultAsync(model => model.DmtareaId == dmtareaid)
                is Dmtarea model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDmtareaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int dmtareaid, Dmtarea dmtarea, DMAPIGestorDeTareasMagicContext db) =>
        {
            var affected = await db.Dmtarea
                .Where(model => model.DmtareaId == dmtareaid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.DmtareaId, dmtarea.DmtareaId)
                    .SetProperty(m => m.Dmtitulo, dmtarea.Dmtitulo)
                    .SetProperty(m => m.Dmdescripcion, dmtarea.Dmdescripcion)
                    .SetProperty(m => m.DmfechaVencimiento, dmtarea.DmfechaVencimiento)
                    .SetProperty(m => m.DmprioridadId, dmtarea.DmprioridadId)
                    .SetProperty(m => m.DmcategoriaId, dmtarea.DmcategoriaId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDmtarea")
        .WithOpenApi();

        group.MapPost("/", async (Dmtarea dmtarea, DMAPIGestorDeTareasMagicContext db) =>
        {
            db.Dmtarea.Add(dmtarea);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Dmtarea/{dmtarea.DmtareaId}",dmtarea);
        })
        .WithName("CreateDmtarea")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int dmtareaid, DMAPIGestorDeTareasMagicContext db) =>
        {
            var affected = await db.Dmtarea
                .Where(model => model.DmtareaId == dmtareaid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDmtarea")
        .WithOpenApi();
    }
}
