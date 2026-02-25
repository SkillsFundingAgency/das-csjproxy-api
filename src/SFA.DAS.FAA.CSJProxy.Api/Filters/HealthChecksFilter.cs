using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SFA.DAS.FAA.CSJProxy.Api.Filters;

[ExcludeFromCodeCoverage]
public class HealthChecksFilter : IDocumentFilter
{
    private const string HealthCheckEndpoint = "/health";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var operation = new OpenApiOperation
        {
            Tags = new HashSet<OpenApiTagReference>([new OpenApiTagReference(HealthCheckEndpoint, swaggerDoc)])
        };

        var healthyResponse = new OpenApiResponse()
        {
            Content = new Dictionary<string, OpenApiMediaType>()
            {
                ["text/plain"] = new()
                {
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String,
                        Enum = ["Healthy"]
                    }
                }
            }
        };
        
        operation.Responses!.Add("200", healthyResponse);

        var unhealthyResponse = new OpenApiResponse()
        {
            Content = new Dictionary<string, OpenApiMediaType>()
            {
                ["text/plain"] = new()
                {
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String,
                        Enum = ["Unhealthy"]
                    }
                }
            }
        };

        operation.Responses.Add("503", unhealthyResponse);
        var pathItem = new OpenApiPathItem();
        pathItem.AddOperation(HttpMethod.Get, operation);
        swaggerDoc?.Paths.Add(HealthCheckEndpoint, pathItem);
    }
}