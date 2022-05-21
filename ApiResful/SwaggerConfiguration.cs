using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace ApiResful
{
    public class SwaggerConfiguration : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerConfiguration(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        private OpenApiInfo createVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "My Api Retful",
                Version = description.ApiVersion.ToString(),
                Description = "This is my Second Api Versioning control",
                Contact = new OpenApiContact()
                {
                    Email = "franklynferrera@gmail.com",
                    Name = "Franklyn",
                }
            };
            if (description.IsDeprecated)
            {
                info.Description += "This Api version has been deprecated";
            }
            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            //Add Swagger Documentation for each API version we have 
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, createVersionInfo(description));
            }
        }



        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }


    }
}

