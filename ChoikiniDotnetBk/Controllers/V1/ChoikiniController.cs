using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ChoikiniDotnetBk.Utils;

namespace ChoikiniDotnetBk.Controllers.V1
{
     [Route("api/v1/[controller]")]
    [ApiController]
    public class ChoikiniController: ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
                var defaultResponse = new{
                    IdValue = "v1-0002",
                    Content = "これは/choikiniに対するGetリクエストでした"
                };

            return ResponseUtil.CreateJsonResult(defaultResponse);
        }


        // TODO /choikini/:userのPOST
        // TODO /choikini/:userのGET
        // TODO /choikini/のGET
    }
}