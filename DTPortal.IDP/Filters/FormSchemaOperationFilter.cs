using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace AppShieldRestAPICore.Filters
{
    public class FormSchemaOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody == null)
                return;

            operation.RequestBody.Required = true;

            var formContent = operation.RequestBody.Content
                .FirstOrDefault(c => c.Key == "application/x-www-form-urlencoded");

            if (formContent.Value?.Schema == null)
                return;

            if (formContent.Value.Schema is not OpenApiSchema schema)
                return;

            schema.AdditionalPropertiesAllowed = false;

            var formParam = context.MethodInfo
                .GetParameters()
                .FirstOrDefault(p =>
                    p.GetCustomAttribute<Microsoft.AspNetCore.Mvc.FromFormAttribute>() != null);

            if (formParam == null)
                return;

            var requiredProperties = formParam.ParameterType
                .GetProperties()
                .Where(p =>
                    p.GetCustomAttribute<RequiredAttribute>() != null &&
                    p.GetMethod?.IsPublic == true)
                .Select(p =>
                {
                    var jsonProp = p.GetCustomAttribute<JsonPropertyNameAttribute>();
                    return jsonProp != null
                        ? jsonProp.Name
                        : char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1);
                })
                .ToList();

            schema.Required ??= new HashSet<string>();

            foreach (var prop in requiredProperties)
            {
                if (schema.Properties.ContainsKey(prop))
                {
                    schema.Required.Add(prop);
                }
            }
        }
    }
}
