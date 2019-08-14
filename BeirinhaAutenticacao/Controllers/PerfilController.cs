using BeirinhaAutenticacao.Models;
using BeirinhaAutenticacao.Utils;
using BeirinhaAutenticacao.ViewModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace BeirinhaAutenticacao.Controllers
{
    public class PerfilController : Controller
    {

        private UsuarioContext _usuarioContext = new UsuarioContext();

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            var usuario = _usuarioContext.Usuarios.FirstOrDefault(u => u.Login == login);

            if(Hash.GerarHash(viewModel.SenhaAtual) != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta!");
                return View();
            }

            usuario.Senha = Hash.GerarHash(viewModel.NovaSenha);
            _usuarioContext.Entry(usuario).State = EntityState.Modified;
            _usuarioContext.SaveChanges();

            TempData["Mensagem"] = "Senha alterada com sucesso. Logue com a senha nova.";
            return RedirectToAction("Index", "Painel"); 
        }
    }
}