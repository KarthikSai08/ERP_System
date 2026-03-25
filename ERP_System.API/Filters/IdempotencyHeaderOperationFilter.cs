using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ERP_System.API.Filters
{
    public class IdempotencyHeaderOperationFilter : IOperationFilter
    {
        private static readonly HashSet<string> _methods = new()
        {
            "POST", "PUT", "DELETE"
        };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var method = context.ApiDescription.HttpMethod?.ToUpper();

            if (!_methods.Contains(method ?? ""))
                return;

            if (operation.Parameters == null)
                operation.Parameters = new List<IOpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Idempotency-Key",
                In = ParameterLocation.Header,
                Required = false,
                Description = "Unique key to prevent duplicate requests (e.g. a UUID)"
            });
        }
    }
}