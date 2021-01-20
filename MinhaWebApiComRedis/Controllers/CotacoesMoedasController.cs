using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using MinhaWebApiComRedis.Models;
using Microsoft.Extensions.Logging;
using MinhaWebApiComRedis.Database;
using static MinhaWebApiComRedis.Models.CotacoesBaseLayout;

namespace MinhaWebApiComRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacoesMoedasController : ControllerBase
    {
        private readonly ILogger<CotacoesMoedasController> _logger;
        private readonly ApiContext _dbContext;

        public CotacoesMoedasController(ILogger<CotacoesMoedasController> logger,
            ApiContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost("/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create()
        {
            var rng = new Random();

            var cotacoes = Enumerable.Range(0, NomeMoedas.Length -1).Select(index => new CotacoesMoedas
            {
                Data = DateTime.Now.AddDays(index * -1),
                NomeMoeda = NomeMoedas[index],
                SiglaMoeda = SiglasMoedas[index],
                Valor = ValoresMoedas[index]
            })
            .ToArray();

            await _dbContext.CotacoesMoedas.AddRangeAsync(cotacoes);
            await _dbContext.SaveChangesAsync();

            return Created("create", cotacoes);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<CotacoesMoedas> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new CotacoesMoedas
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
