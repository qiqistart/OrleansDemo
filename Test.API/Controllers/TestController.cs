using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansGrains.DemoGrains.DemoIGrains;

namespace Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private IClusterClient clusterClient;
        public TestController(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task Test()
        {
            await clusterClient.GetGrain<IHalloGrains>(Guid.NewGuid().ToString()).Hallo();
        }
    }
}
