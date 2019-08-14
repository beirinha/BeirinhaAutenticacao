using BeirinhaAutenticacao.Models;
using BeirinhaAutenticacao.ViewModel;
using System.Web.Mvc;
using BeirinhaAutenticacao.Utils;
using System.Linq;
using System.Web;
using System.Security.Claims;

namespace BeirinhaAutenticacao.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UsuarioContext db = new UsuarioContext();

        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuarioViewModel cadastroUsuarioViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(cadastroUsuarioViewModel);
            }

            if(db.Usuarios.Count(u => u.Login == cadastroUsuarioViewModel.Login) > 0)
            {
                ModelState.AddModelError("Login", "Esse usuário já existe");
                return View(cadastroUsuarioViewModel);
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = cadastroUsuarioViewModel.Nome,
                Login = cadastroUsuarioViewModel.Login,
                Senha = Hash.GerarHash(cadastroUsuarioViewModel.Senha)
            };

            db.Usuarios.Add(novoUsuario);
            db.SaveChanges();

            TempData["Mensagem"] = "Cadastro realizado com sucesso. Efetue Login";
            return RedirectToAction("Login");
        }

        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = db.Usuarios.FirstOrDefault(x => x.Login == model.Login);

            if(usuario == null)
            {
                ModelState.AddModelError("Login", "Usuario não encontrado");
                return View(model);
            }

            if(usuario.Senha != Hash.GerarHash(model.Senha))
            {
                ModelState.AddModelError("Login", "Senha incorreta");
                return View(model);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login)
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if(!string.IsNullOrEmpty(model.UrlRetorno) || Url.IsLocalUrl(model.UrlRetorno))
            {
                return Redirect(model.UrlRetorno);
            }
            else
            {
                return RedirectToAction("Index", "Painel");
            }
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}