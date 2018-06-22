using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace ApiDevMyGame.SwaggerExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthTokenOperation : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiExplorer"></param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/oauth2/tokens", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> { "Tokens" },
                    summary = "Autenticar usuário para concessão de token de autorização (válido por 30 minutos)",
                    consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },
                    parameters = new List<Parameter> {
                        new Parameter
                        {
                            type = "string",
                            name = "grant_type",
                            description = "Tipo de acesso a ser solicitado (apenas o tipo 'password' é aceito)",
                            required = true,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "username",
                            description = "E-mail informado no cadastro do usuário",
                            required = false,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "password",
                            description = "Senha informada no cadastro do usuário",
                            required = false,
                            @in = "formData"
                        }
                    }
                }
            });
        }
    }
}