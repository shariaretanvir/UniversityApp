using System.Text.Json;

namespace StudentApp.API.Common
{
    public static class StaticDeclaration
    {
        public static readonly JsonSerializerOptions camelCase = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
