using Domain.Entities;
using Domain.Interfaces.Services;
using Sat.Recruitment.API.Extentions;
using System.Reflection;

namespace Sat.Recruitment.API.Controllers;

public static class UserController
{
    ///<summary>
    /// Conjutos de controladores para las operaciones con los perfiles de la aplicacion
    /// </summary>
    public static void UserRoutes(this WebApplication app)
    {
        ///<summary>
        /// Controlador que realiza la creacion de los usuarios
        /// </summary>
        app.MapPost("api/user", async (IUserService service, User user) => Results.Extensions.ResultResponse(await service.CreateUser(user)));
        /// <summary>
        /// Controlador que retorna todos los usuarios existentes en la BD. 
        /// </summary>
        /// <param></param>
        app.MapGet("api/user", async (IUserService moduleService) => Results.Extensions.ResultResponse(await moduleService.GetAllUser()));
        /// <summary>
        /// Controlador que actualiza un usuario
        /// </summary>
        /// <param></param>
        app.MapPut("api/user", async (IUserService moduleService, User user) => Results.Extensions.ResultResponse(await moduleService.UpdateUser(user)));

    }
}
