using GraphQL.Types;
using MinhaWebApiComRedis.Models;

namespace MinhaWebApiComRedis.GraphQL.Types
{
    public class CotacoesMoedasType : ObjectGraphType<CotacoesMoedas>
    {
        public CotacoesMoedasType()
        {
            Name = "cotacoes";

            Field(x => x.Id, type: typeof(IdGraphType))
                .Description("Id da entidade CotacoesMoedas");

            Field(x => x.NomeMoeda)
                .Description("Propriedade NomeMoeda da entidade CotacoesMoedas");

            Field(x => x.SiglaMoeda)
                .Description("Propriedade SiglaMoeda da entidade CotacoesMoedas");

            Field(x => x.Data)
                .Description("Propriedade Data da entidade CotacoesMoedas");

            Field(x => x.Valor)
                .Description("Propriedade Valor da entidade CotacoesMoedas");
        }
    }
}
