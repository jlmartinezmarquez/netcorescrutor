using System.Collections.Generic;
using IntegratedIocContainerApi.Extensions;
using IntegratedIocContainerApi.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.BasicOperations;
using Services.ComplexOperations;

namespace IntegratedIocContainerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IComplexOperationA complexOperationA;
        private readonly IComplexOperationB complexOperationB;

        private readonly IBasicOperations basicOperations;

        public ValuesController(
            IComplexOperationA complexOperationA, 
            IComplexOperationB complexOperationB,
            IOptions<OperationsSettings> settings,
            IEnumerable<IBasicOperations> basicOperationsEnumerable)
        {
            this.complexOperationA = complexOperationA;
            this.complexOperationB = complexOperationB;

            var basicOperatiosClassFromSettings = settings.Value.CurrentBasicOperations;
            basicOperations = basicOperationsEnumerable.ResolveClass(basicOperatiosClassFromSettings);
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
