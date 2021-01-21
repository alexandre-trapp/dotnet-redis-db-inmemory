using System;
using GraphQL.Types;
using GraphQL.Utilities;
using MinhaWebApiComRedis.GraphQL.Queries;

namespace MinhaWebApiComRedis.GraphQL.Schemes
{
    public class ApiScheme : Schema
    {
        public ApiScheme(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<ApiQuery>();
        }
    }
}
