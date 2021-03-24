using ErLiang.JsonReMap;
using ErLiang.Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Teld.BSS.Notice_V2.Domain.JsonRemap.Tests
{
    [TestClass()]
    public class ContextHandleTests
    {
        private string msg = FileHelper.ReadJsonFromFile("WideTableSendCase2.json");
        private JObject refer = JObject.Parse(FileHelper.ReadJsonFromFile("mappingResult.json"));
        [TestMethod()]
        public void GetJsonTest()
        {
            ContextHandle handle = new ContextHandle();

            List<MappingContext> contexts = new List<MappingContext>
            {
                new MappingContext
                {
                    MappingTerms = new List<JsonMappingTerm>
                    {
                        new JsonMappingTerm
                        {
                            Source = "Sender",
                            Target = "myInfo"
                        }
                    },
                    Context = new List<MappingContext>
                    {
                        new MappingContext
                        {
                            SLocation = "NoticeDetail.NoticeContext{}",
                            TLocation = "myOrder",
                            MappingTerms = new List<JsonMappingTerm>
                            {
                                new JsonMappingTerm
                                {
                                    Source = "ReceiptDataVM",
                                    Target = "receiptInfo"
                                }

                            },
                            Context =new List<MappingContext>
                            {
                                new MappingContext
                                {
                                    SLocation = "ReceiptDataVM[]",
                                    TLocation = "Arrtest",
                                    MappingTerms = new List<JsonMappingTerm>
                                    {
                                        new JsonMappingTerm
                                        {
                                            Source = "ID",
                                            Target = "uuid"
                                        },
                                        new JsonMappingTerm
                                        {
                                            Source = "nothis",
                                            Target = "nothis"
                                        },
                                        new JsonMappingTerm
                                        {
                                            Source = "Code",
                                            Target = "Code"
                                        }
                                    }
                                },
                                new MappingContext
                                {
                                    SLocation = "OrderVM{}",
                                    TLocation = "",
                                    MappingTerms = new List<JsonMappingTerm>
                                    {
                                        new JsonMappingTerm
                                        {
                                            Source = "BasicData",
                                            Target = "BasicData"
                                        },
                                        new JsonMappingTerm
                                        {
                                            Source = "bizMechantData",
                                            Target = "bizMechantData"
                                        },
                                        new JsonMappingTerm
                                        {
                                            Source = "bizCarDataList",
                                            Target = "aliabizCarDataList"
                                        },
                                        new JsonMappingTerm
                                        {
                                            Source = "bizPolicyDataList",
                                            Target = "bizPolicyDataList"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Console.WriteLine(JsonConvert.SerializeObject(contexts));
            string target = handle.GetJson(msg, contexts);
            Console.WriteLine(target);
            JObject actual = JObject.Parse(target);
            CompareDic(actual, refer);

        }

        private void CompareDic(JObject act, JObject refer)
        {
            foreach (KeyValuePair<string, JToken> item in refer)
            {
                if (item.Value is JValue)
                {
                    try
                    {
                        Assert.AreEqual(item.Value.ToString(), act[item.Key].ToString());
                    }
                    catch (AssertFailedException e)
                    {
                        throw new AssertFailedException($"断言失败，{e.Message},位置{item.Key}");
                    }
                }
                else if (item.Value is JObject)
                {
                    CompareDic((JObject)act[item.Key], (JObject)item.Value);
                }

            }
        }
    }
}