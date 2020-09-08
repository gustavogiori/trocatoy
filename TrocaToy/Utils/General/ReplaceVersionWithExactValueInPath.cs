using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace TrocaToy.Utils.General
{
    public class ReplaceVersionWithExactValueInPath : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = (OpenApiPaths)swaggerDoc.Paths
              .ToDictionary(
                  path => path.Key.Replace("v{version}", swaggerDoc.Info.Version),
                  path => path.Value
              );
        }
    }
}
