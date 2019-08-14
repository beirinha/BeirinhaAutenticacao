using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BeirinhaAutenticacao.ViewModel
{
    public class LoginViewModel
    {
        [HiddenInput]
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        [MaxLength(6, ErrorMessage = "A senha deve ter até 50 Caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        [DataType(DataType.Password)]       
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 Caracteres.")]
        public string Senha { get; set; }

    }
}