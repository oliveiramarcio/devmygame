using Aplicacao.Interfaces.Cadastros;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiDevMyGame.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var usuarioAppService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUsuarioAppService)) as IUsuarioAppService;

            bool usuarioAutenticado = usuarioAppService.AutenticarUsuario(context.UserName, context.Password);

            if (!usuarioAutenticado)
            {
                context.SetError("usuario_invalido", "Usuário inválido!");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            IdentityUser idUsuario = new IdentityUser()
            {
                UserName = context.UserName,
                PasswordHash = context.Password,
                Email = context.UserName,
                EmailConfirmed = true
            };

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, idUsuario), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, IdentityUser user)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            var usuarioAppService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUsuarioAppService)) as IUsuarioAppService;

            const string nomePerfil = "geral";

            identity.AddClaim(new Claim(ClaimTypes.Role, nomePerfil));

            return identity;
        }
    }
}