using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http.Description;

namespace ApiDevMyGame.SwaggerExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class SortOperationsByPathDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiExplorer"></param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var paths = swaggerDoc.paths.OrderBy(e => this.GetOperationFilterString(e.Key, e.Value)).ToList();

            swaggerDoc.paths = paths.ToDictionary(e => e.Key, e => e.Value);
        }

        private string GetOperationFilterString(string key, PathItem value)
        {
            var groupByName = string.Empty;

            if (value.get?.tags != null)
            {
                groupByName = value.get.tags[0];
            }
            else if (value.delete?.tags != null)
            {
                groupByName = value.delete.tags[0];
            }
            else if (value.patch?.tags != null)
            {
                groupByName = value.patch.tags[0];
            }
            else if (value.post?.tags != null)
            {
                groupByName = value.post.tags[0];
            }
            else if (value.put?.tags != null)
            {
                groupByName = value.put.tags[0];
            }

            if (!string.IsNullOrWhiteSpace(groupByName))
            {
                return groupByName + '-' + key;
            }

            return value.ToString();
        }
    }
}