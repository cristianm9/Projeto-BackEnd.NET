using Curso_DIO.Models.Usuarios;

namespace Curso_DIO.Configurations
{
    public interface IAuthenticationService
    {
       string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
