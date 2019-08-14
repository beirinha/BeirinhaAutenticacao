using System.ComponentModel.DataAnnotations;

namespace BeirinhaAutenticacao.ViewModel
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Senha atual invalida")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        [MinLength(6, ErrorMessage = "A senha deve ter 6 Caracteres.")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Confirme sua Nova Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter 6 Caracteres.")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Repita sua Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter 6 Caracteres.")]
        [Compare(nameof(NovaSenha), ErrorMessage = "A senha e a confirmação não estão iguais.")]
        public string ConfirmacaoSenha { get; set; }
    }
}