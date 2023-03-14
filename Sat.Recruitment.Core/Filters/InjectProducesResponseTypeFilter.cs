using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sat.Recruitment.Web.Filters
{
    public class InjectProducesResponseTypeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            Add_401Unauthorized(ref operation, ref context);

            Add_403Forbidden(ref operation, ref context);

            Add_400BadRequest(ref operation, ref context);

            Add_500InternalServerError(ref operation, ref context);

        }
        private static void AddResponse(ref OpenApiOperation operation, ref OperationFilterContext context, int statusCode, Type? typeOf)
        {
            var response = new OpenApiResponse
            {
                Description = ReasonPhrases.GetReasonPhrase(statusCode)
            };

            if (typeOf != null)
            {
                response.Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = context.SchemaGenerator.GenerateSchema(typeOf, context.SchemaRepository)
                    }
                };
            }

            operation.Responses.TryAdd(statusCode.ToString(), response);
        }

        #region Methods for add responses

        private static void Add_400BadRequest(ref OpenApiOperation operation, ref OperationFilterContext context)
        {
            AddResponse(ref operation, ref context, StatusCodes.Status400BadRequest, null);
        }
        private static void Add_401Unauthorized(ref OpenApiOperation operation, ref OperationFilterContext context)
        {
            AddResponse(ref operation, ref context, StatusCodes.Status401Unauthorized, null);
        }
        private static void Add_403Forbidden(ref OpenApiOperation operation, ref OperationFilterContext context)
        {
            AddResponse(ref operation, ref context, StatusCodes.Status403Forbidden, null);
        }
        private static void Add_500InternalServerError(ref OpenApiOperation operation, ref OperationFilterContext context)
        {
            AddResponse(ref operation, ref context, StatusCodes.Status500InternalServerError, null);
        }

        #endregion
    }
}


