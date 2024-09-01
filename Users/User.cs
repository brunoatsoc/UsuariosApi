namespace UsuariosApi.Users;

public class User {
    public Guid User_Id {get; init;}
    public required string Full_Name {get; set;}
    public required string User_Name {get; set;}
    public required string Password {get; set;}
}