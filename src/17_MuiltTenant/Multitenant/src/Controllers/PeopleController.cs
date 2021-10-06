using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Multitenant.Data;
using Multitenant.Domain;

namespace EFCore.Multitenant.Controllers
{
    [ApiController]
    [Route("{tenant}/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ILogger<PeopleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get([FromServices]ApplicationContext db)
        {
            var people = db.People.ToArray();
            return people;
        }
    }
}
