using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using MinhaWebApiComRedis.Models;
using Microsoft.Extensions.Logging;
using MinhaWebApiComRedis.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CotacoesMoedas>>> Get(
            [FromServices]IDistributedCache redisCache)
        {
            string cacheCotacoesJson = await redisCache.GetStringAsync("CotacoesMoedas");

            IEnumerable<CotacoesMoedas> cotacoes;

            if (!string.IsNullOrEmpty(cacheCotacoesJson))
            {
                cotacoes = JsonSerializer.Deserialize<IEnumerable<CotacoesMoedas>>(cacheCotacoesJson);
                cotacoes.ToList().ForEach(x => x.Mensagem = "Do cache redis");

                return Ok(cotacoes);
            }

            cotacoes = await _dbContext.CotacoesMoedas.ToListAsync();
            cotacoes.ToList().ForEach(x => x.Mensagem = "Do banco de dados.");

            if (cotacoes.Any())
            {
                cacheCotacoesJson = JsonSerializer.Serialize(cotacoes);

                var opcoesCache = new DistributedCacheEntryOptions();
                opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                await redisCache.SetStringAsync("CotacoesMoedas", cacheCotacoesJson, opcoesCache);
            }

            return Ok(cotacoes);
        }
    }
}
