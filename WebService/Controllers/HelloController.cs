using Common;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IService service;

        public HelloController(IService service = null)
        {
            this.service = service ?? new Server();
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return this.service.SayHello();
        }
    }
}
