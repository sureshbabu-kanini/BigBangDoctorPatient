using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class SwaggerFileUploadFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            return;
        }

        foreach (var parameter in operation.Parameters)
        {
            var isFileUpload = parameter.Name.ToLowerInvariant().Contains("file") && parameter.In == ParameterLocation.Query;
            if (isFileUpload)
            {
                parameter.Required = true;
                parameter.Schema = new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                };
                parameter.Description = "Select a file to upload";
                parameter.Extensions.Add("x-ms-media-kind", new OpenApiString("image"));
            }
        }
    }
}
