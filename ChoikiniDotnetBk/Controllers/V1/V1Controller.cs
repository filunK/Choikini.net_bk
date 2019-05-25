using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChoikiniDotnetBk.Controllers.V1
{
     [Route("api/v1")]
    [ApiController]
    public class V1Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new NotFoundResult();
        }

        
    }
}