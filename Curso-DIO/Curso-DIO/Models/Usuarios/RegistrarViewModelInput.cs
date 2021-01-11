using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso_DIO.Models.Usuarios
{
    public class RegistrarViewModelInput
    {
        [Required(ErrorMessage = "O login é Obrigatório")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "O E-mail é Obrigatório")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O login é Obrigatório")]
        public string Senha { get; set; }
    }
}
