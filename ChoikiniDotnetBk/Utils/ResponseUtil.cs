using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Hal.Builders;
using Hal;

using ChoikiniDotnetBk.Models;

namespace ChoikiniDotnetBk.Utils
{
    /*
     * コントローラのレスポンスを扱うユーティリティクラス
     */
    public class ResponseUtil
    {
        public static JsonResult CreateJsonResult(object content){

            // JSON生成の設定
            var jsonSetting = new JsonSerializerSettings();
            jsonSetting.Formatting = Formatting.Indented;
            jsonSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return new JsonResult(content, jsonSetting);
        }

        /*
         * HALフォーマットに合わせたJsonResultを生成します。
         */
        public static JsonResult GenerateHal(IHalContent content) {

            var resource = new Resource();
            resource.State = new {
                state = content.State.ToString(),
                stateDetail = content.StateDetail,
                response = content.Response,
            };

            resource.Links = new LinkCollection {
                new Link("self") {
                    Items = new LinkItemCollection {
                        new LinkItem(content.SelfUri)
                    }
                }
            };

            if (content.Embedded != null && content.Embedded.Length > 0)
            {
                var embeddedResources = new EmbeddedResourceCollection();
                
                Array.ForEach(content.Embedded, embedded => {

                    var embeddedResource = new EmbeddedResource();
                    embeddedResource.Name = embedded.Name;
                    embeddedResource.Resources = new ResourceCollection();

                    Array.ForEach(embedded.EmbeddedContent, embeddedContent => {

                        var res = new Resource(embeddedContent.EmbeddedData);

                        res.Links = new LinkCollection {
                            new Link("self") {
                                Items = new LinkItemCollection {
                                    new LinkItem(embeddedContent.SelfUri)
                                }
                            }
                        };
                        embeddedResource.Resources.Add(res);

                    });

                    embeddedResources.Add(embeddedResource);
                });

                resource.EmbeddedResources = embeddedResources;
            }

            var rawJson = resource.ToString();
            var jsonResult = ResponseUtil.CreateJsonResult(JsonConvert.DeserializeObject(rawJson));

            jsonResult.ContentType = "application/hal+json";
            return jsonResult;
        }

        public static JsonResult GenerateSampleHalJson() {
            var builder = new ResourceBuilder();
            var instruction = builder
                .WithState(new { currentlyProcessing = 14, shippedToday = 20 })
                .AddSelfLink().WithLinkItem("/orders")
                .AddCuriesLink().WithLinkItem("http://example.com/docs/rels/{rel}", "ea", true)
                .AddLink("next").WithLinkItem("/orders?page=2")
                .AddLink("ea:find").WithLinkItem("/orders{?id}", templated: true)
                .AddEmbedded("ea:order")
                    .Resource(new ResourceBuilder()
                        .WithState(new { total = 30.00F, currency = "USD", status = "shipped" })
                        .AddSelfLink().WithLinkItem("/orders/123")
                        .AddLink("ea:basket").WithLinkItem("/baskets/98712")
                        .AddLink("ea:customer").WithLinkItem("/customers/7809"))
                    .Resource(new ResourceBuilder()
                        .WithState(new { total = 20.00F, currency = "USD", status = "processing" })
                        .AddSelfLink().WithLinkItem("/orders/124")
                        .AddLink("ea:basket").WithLinkItem("/baskets/97213")
                        .AddLink("ea:customer").WithLinkItem("/customers/12369"))
                .Build();

            var rawjson = instruction.ToString();

            var resource = JsonConvert.DeserializeObject(rawjson);

            return ResponseUtil.CreateJsonResult(resource);
        }

    }
}