using System.ComponentModel.DataAnnotations;

namespace BeirinhaAutenticacao.ViewModel
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage ="Informa seu Nome")]
        [MaxLength(100, ErrorMessage = "Seu nome só pode ter 100 Caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informa seu Login")]
        [MaxLength(100, ErrorMessage = "Seu nome só pode ter 50 Caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informa seu Senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter 6 Caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Repita sua Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter 6 Caracteres.")]
        [Compare(nameof(Senha), ErrorMessage = "S senha e a confirmação não estão iguais.")]
        public string ConfirmacaoSenha { get; set; }
    }
}