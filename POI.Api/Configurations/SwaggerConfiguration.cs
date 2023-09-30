using Microsoft.OpenApi.Models;

namespace POI.Api.Configurations;

public static class SwaggerConfiguration
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {

        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type =>
            {
                if (type.IsNested)
                {
                    return $"{type.Namespace}.{type.DeclaringType.Name}.{type.Name}";
                }

                return $"{type.Namespace}.{type.Name}";
            });
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Poi Api", Version = "v1"});

            c.SwaggerDoc("experimental",
                new OpenApiInfo
                {
                    Title = "Experimental Api",
                    Description = "These endpoints are experimental please use versioned endpoints",
                    Version = "experimental"
                });

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (docName == "experimental" && apiDesc.RelativePath.Contains("experimental"))
                    return true;
                if (docName == "v1" && !apiDesc.RelativePath.Contains("experimental"))
                    return true;
                return false;
            });
        });
    }
}
