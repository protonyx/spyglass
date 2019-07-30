using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;
using Spyglass.SDK.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spyglass.Server.Controllers
{
    [Route("metrics")]
    public class PrometheusController : Controller
    {

        protected IRepository<Metric> MetricRepository { get; }

        public PrometheusController(IDataContext dataContext)
        {
            MetricRepository = dataContext.Repository<Metric>();
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var output = new StringBuilder();
            var metrics = this.MetricRepository.GetAll();

            foreach (var metric in metrics)
            {
                var provider = ProviderService.BuildProvider(metric.ProviderType, metric.Provider);
                var values = await provider.GetValueAsync();

                foreach (var value in values)
                {
                    output.AppendFormat("{0}_{1} {2}", metric.Name, value.Name.Replace(" ", "_"), value.Value);
                    output.AppendLine();
                }
                
            }

            return Content(output.ToString());
        }
    }
}
