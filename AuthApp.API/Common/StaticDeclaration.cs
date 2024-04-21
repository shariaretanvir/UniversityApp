using System.Text.Json;

namespace AuthApp.API.Common
{
    public class StaticDeclaration
    {
        public static readonly JsonSerializerOptions camelCase = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }
}
