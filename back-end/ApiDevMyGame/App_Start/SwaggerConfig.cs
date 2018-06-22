using ApiDevMyGame.App_Start;
using ApiDevMyGame.SwaggerExtensions;
using Biblioteca.Classes;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ApiDevMyGame.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ApiDevMyGame");
                    
                    c.GroupActionsBy(apiDesc => apiDesc.GetControllerAndActionAttributes<GroupSwagger>().Any()
                        ? apiDesc.GetControllerAndActionAttributes<GroupSwagger>().First().GroupName
                        : apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName);

                    c.OrderActionGroupsBy(new StringComparerAscending());

                    c.IncludeXmlComments(String.Format("{0}\\bin\\ApiDevMyGame.xml",
                        AppDomain.CurrentDomain.BaseDirectory));

                    c.DescribeAllEnumsAsStrings();

                    c.DocumentFilter<AuthTokenOperation>();

                    c.DocumentFilter<SortOperationsByPathDocumentFilter>();
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(thisAssembly,
                        "ApiDevMyGame.SwaggerExtensions.AddBearerAuthHeader.js");
                });
        }
    }
}