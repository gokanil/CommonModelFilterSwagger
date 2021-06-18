using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Process.SwaggerFilter
{
    public class SwaggerSkipPropertyFilter : ISchemaFilter, IOperationFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
                return;

            var skipProperties = context.Type.GetProperties();

            foreach (var skipProperty in skipProperties)
            {
                var ignoreAttribute = skipProperty.GetCustomAttribute<SwaggerIgnoreAttribute>();

                if (ignoreAttribute != null && !ignoreAttribute.ExceptUpdate)
                {
                    var propertyToSkip = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, skipProperty.Name, StringComparison.OrdinalIgnoreCase));

                    if (propertyToSkip != null)
                        schema.Properties.Remove(propertyToSkip);
                }
            }
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionName = (context.ApiDescription.ActionDescriptor as ControllerActionDescriptor).ActionName;
            if (actionName.StartsWith("Create"))
            {
                foreach (ParameterInfo parameter in context.MethodInfo.GetParameters())
                {
                    //IDictionary<string, OpenApiSchema> properties = context.SchemaRepository.Schemas[parameter.ParameterType.FullName].Properties;

                    IDictionary<string, OpenApiSchema> properties = new Dictionary<string, OpenApiSchema>();
                    foreach (var property in context.SchemaRepository.Schemas[parameter.ParameterType.Name].Properties)
                    {
                        properties.Add(property);
                    }

                    PropertyInfo[] modelProperties = parameter.ParameterType.GetProperties();
                    foreach (PropertyInfo property in modelProperties)
                    {
                        SwaggerIgnoreAttribute ignoreAttribute = property.GetCustomAttribute<SwaggerIgnoreAttribute>();
                        if (ignoreAttribute != null && ignoreAttribute.ExceptUpdate)
                        {
                            string deleted = properties.Keys.SingleOrDefault(x => string.Equals(x, property.Name, StringComparison.OrdinalIgnoreCase));

                            if (deleted != null)
                                properties.Remove(deleted);
                        }
                    }

                    foreach (var content in operation.RequestBody.Content)
                    {
                        content.Value.Schema.Reference = null;
                        content.Value.Schema.Properties = properties;
                    }
                }
            }
        }
    }
}
