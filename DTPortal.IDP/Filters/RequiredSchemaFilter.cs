using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppShieldRestAPICore.Filters
{
    public class RequiredSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == null || schema.Properties == null)
                return;

            // ✅ Apply to ALL objects (including ProfileInfo)
            schema.AdditionalPropertiesAllowed = false;

            schema.Required ??= new HashSet<string>();

            var requiredProperties = context.Type
                .GetProperties()
                .Where(p =>
                    Attribute.IsDefined(p, typeof(RequiredAttribute)) &&
                    p.GetMethod?.IsPublic == true)
                .Select(p =>
                    char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1));

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