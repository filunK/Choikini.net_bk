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

       public ResponseState State{get;}

        public string StateDetail{get;}

        public object [] Response{get;}

        public IEmbeddedModel[] Embedded{get; set;}

        public TestContent() {
            this.SelfUri = "/api";

                this.State = ResponseState.OK;
                this.StateDetail = String.Empty;
                this.Response = new object[]{
                    new {
                        IdValue = "v0-0001",
                        Content = "これは/userに対するGetリクエストでした"
                    }
                };

            this.Embedded = new IEmbeddedModel[]{
                new TestEmbedded() {
                    Name = "testEmbedded",
                    EmbeddedContent = new IEmbeddedContent[] {
                        new TestEmbeddedContent() {
                            SelfUri = "./",
                            EmbeddedData = new {
                                EmbeddedKey = "123456",
                                EmbeddedValue = "ああああああ"
                            }
                        },
                    }
                }
            };
        }
    }

    public class TestEmbedded: IEmbeddedModel {
        public string Name{get; set;}

        public IEmbeddedContent[] EmbeddedContent{get; set;}

    }

    public class TestEmbeddedContent: IEmbeddedContent
    {
        public string SelfUri{get; set;}

        public object EmbeddedData{get; set;}

    }
}