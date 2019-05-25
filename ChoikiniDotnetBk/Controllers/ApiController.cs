using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ChoikiniDotnetBk.Utils;
using ChoikiniDotnetBk.Models;

namespace ChoikiniDotnetBk.Controllers
{
     [Route("api")]
    [ApiController]
    public class ApiController
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var responsedata = new TestContent();
            return ResponseUtil.GenerateHal(responsedata);
        }

        
    }

    public class TestContent: IHalContent {
        public string SelfUri{get; set;}
        public IEmbeddedModel Embedded{get; set;}

        public TestContent() {
            this.SelfUri = "/api";

            this.Embedded = new TestEmbedded(){
                state = ResponseState.OK,
                StateDetail = String.Empty,
                Response = new object[]{
                    new {
                        IdValue = "v0-0001",
                        Content = "これは/userに対するGetリクエストでした"
                    }
                }
            };

        }
    }

    public class TestEmbedded: IEmbeddedModel {
    public ResponseState state{get; set;}

    public string StateDetail{get; set;}

    public object [] Response{get; set;}


    }
}