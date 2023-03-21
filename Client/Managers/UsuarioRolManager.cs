using Bienes.Shared.Wrapper;
using Bienes.Shared.Requests;
using Bienes.Shared.Records;
using Bienes.Shared.Routes;
using Bienes.Client.Extensions;
using System.Net.Http.Json;


namespace Bienes.Client.Managers;

public interface IUsuarioRolManager
{
    Task<ResultList<UsuarioRolRecord>> GetAsync();
    Task<Result<int>> CreateAsync(UsuarioRolCreateRequest request);
    Task<Result<UsuarioRolRecord>> GetByIdAsync(int Id);
}

public class UsuarioRolManager : IUsuarioRolManager
{
    private readonly HttpClient httpClient;

    public UsuarioRolManager(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ResultList<UsuarioRolRecord>> GetAsync()
    {
        try
        {
            var response = await httpClient.GetAsync(UsuarioRolRouteManager.BASE);
            var resultado = await response.ToResultList<UsuarioRolRecord>();
            return resultado;
        }
        catch (Exception e)
        {
            return ResultList<UsuarioRolRecord>.Fail(e.Message);
        }
    }
    public async Task<Result<int>> CreateAsync(UsuarioRolCreateRequest request)
    {
       var response = await httpClient.PostAsJsonAsync(UsuarioRolRouteManager.BASE,request);
       return await response.ToResult<int>();
    }
    public async Task<Result<UsuarioRolRecord>> GetByIdAsync(int Id)
    {
       var response = await httpClient.GetAsync(UsuarioRolRouteManager.BuildRoute(Id));
       return await response.ToResult<UsuarioRolRecord>();
    }
}