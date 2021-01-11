using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso_DIO.Models.Usuarios
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "O login é Obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A Senha é Obrigatória")]
        public string Senha { get; set; }
    }
}
