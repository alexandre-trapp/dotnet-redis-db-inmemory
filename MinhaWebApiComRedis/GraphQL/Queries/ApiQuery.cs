using GraphQL.Types;
using MinhaWebApiComRedis.Database;
using MinhaWebApiComRedis.GraphQL.Types;
using System.Linq;

namespace MinhaWebApiComRedis.GraphQL.Queries
{
    public class ApiQuery : ObjectGraphType
    {
        public ApiQuery(ApiContext dbContext)
        {
            Field<ListGraphType<CotacoesMoedasType>>(
                "cotacoes",
                resolve: context => dbContext.CotacoesMoedas.ToList()
            );
        }
    }
}
