using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProducerMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProducerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private ProducerConfig _config;

        public ProducerController(ProducerConfig config)
        {
            _config = config;
        }
        [HttpPost("send")]
        public async Task<ActionResult> Get(string topic, [FromBody] Employee employee)
        {
            string employeeData = JsonConvert.SerializeObject(employee);
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = employeeData });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }

        }
    }
}
