using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using UsuariosApi.Db;
using UsuariosApi.Users;

namespace UsuariosApi.Routes;

public static class Users {
    public static void UserEndPoint(this IEndpointRouteBuilder routes) {
        var userRoutes = routes.MapGroup("/users");

        userRoutes.MapPost("/create_user", async (User req, UsuariosApiContext context) => {
            var alreadExists = await context.Users.FirstOrDefaultAsync(u => u.User_Name == req.User_Name);

            if(alreadExists != null) {
                return Results.Conflict("User alread exists.");
            }

            await context.Users.AddAsync(req);
            await context.SaveChangesAsync();
            return Results.Created();
        });

        userRoutes.MapPut("/user_update/{user_id}", async (Guid user_id, User req, UsuariosApiContext context) => {
            var userUpdate = await context.Users.FirstOrDefaultAsync(u => u.User_Id == user_id);

            if(userUpdate == null) {
                return Results.NotFound("User not found.");
            }

            var alreadExists = await context.Users.FirstOrDefaultAsync(u => u.User_Name == req.User_Name);

            if(alreadExists != null) {
                return Results.Conflict("User alread exists.");
            }

            userUpdate.Full_Name = req.Full_Name;
            userUpdate.User_Name = req.User_Name;
            userUpdate.Password = req.Password;

            await context.SaveChangesAsync();
            return Results.Ok();
        });

        userRoutes.MapGet("/profile/{user_id}", async (Guid user_id, UsuariosApiContext context) => {
            var user = await context.Users.FirstOrDefaultAsync(u => u.User_Id == user_id);

            if(user == null) {
                return Results.NotFound("User not found.");
            }

            return Results.Ok(user);
        });

        userRoutes.MapDelete("/delet_account/{user_id}", async (Guid user_id, UsuariosApiContext context) => {
            var userDelete = await context.Users.FirstOrDefaultAsync(u => u.User_Id == user_id);

            if(userDelete == null) {
                return Results.NotFound("User not found.");
            }

            context.Users.Remove(userDelete);
            await context.SaveChangesAsync();
            return Results.Ok(userDelete);
        });
    }
}