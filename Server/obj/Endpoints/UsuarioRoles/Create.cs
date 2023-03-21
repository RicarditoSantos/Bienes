using System.Data;
using Ardalis.ApiEndpoints;
using Proyecto.Server.Context;
using Proyecto.Shared.Requests;
using Proyecto.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Server.Models;
using Microsoft.EntityFrameworkCore;
using Proyecto.Shared.Routes;

namespace Proyecto.Server.Endpoints.UsuariosRoles;

using Request = UsuarioRolCreateRequest;
using Respuesta = Result<int>;

public class Create : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
{
    private readonly IMyDbContext dbContext;

    public Create(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpPost(UsuarioRolRouteManager.BASE)]
    public override async Task<ActionResult<Respuesta>> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        try
        {
            #region  Validaciones
            var rol = await dbContext.UsuariosRoles.FirstOrDefaultAsync(r => r.Nombre.ToLower() == request.Nombre.ToLower(),cancellationToken); 
            if(rol != null)
                return Respuesta.Fail($"Ya existe un rol con el nombre '({request.Nombre})'.");
            #endregion
            rol = UsuarioRol.Crear(request);
            dbContext.UsuariosRoles.Add(rol);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Respuesta.Success(rol.Id);
        }
        catch(Exception e){
            return Respuesta.Fail(e.Message);
        }
    }
}