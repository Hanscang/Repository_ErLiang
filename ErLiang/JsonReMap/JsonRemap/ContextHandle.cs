using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ErLiang.JsonReMap
{
    /// <summary>
    /// json转换器
    /// </summary>
    /// <remarks>
    /// ContextHandle
    /// 1.查找源位置     提供源json的访问
    /// 2. 根据控制符实例化相应的JsonHelper进行操作
    /// 
    /// mappingSymbol是控制符
    /// 
    /// Jsonhelper  提供对json的操作   具体操作需要分jobject，jarr
    /// 包括：
    /// 1.根据context进行赋值操作
    /// 2.构建目标路径   检测、构建目标
    /// 
    /// </remarks>
    public class ContextHandle
    {
        private int MaxRank { get; set; } = 20;
        private IJHelper jsonHelper = null;

        public string GetJson(string msg, List<MappingContext> contexts)
        {
            if (string.IsNullOrEmpty(msg) || contexts == null) return string.Empty;


            JToken container = JToken.Parse(msg);
            JObject target = new JObject();
            foreach (MappingContext item in contexts)
                PerfTarget(container, target, item);
            return target.ToString();
        }

        public void PerfTarget(JToken container, JToken target, MappingContext context)
        {
            if (MaxRank-- < 0)
                throw new Exception("超出限定最高层级");

            if (container == null || target == null || context == null) return;



            string sLocation;
            if (!string.IsNullOrEmpty(context.SLocation) && context.SLocation.EndsWith(MappingSymbol.ARR))
            {
                sLocation = context.SLocation.Substring(0, context.SLocation.Length - 2);
                jsonHelper = new JArrHelper();
            }
            else if (!string.IsNullOrEmpty(context.SLocation) && context.SLocation.EndsWith(MappingSymbol.ToJson))
            {
                sLocation = context.SLocation.Substring(0, context.SLocation.Length - 2);
                jsonHelper = new JObjHelper();
            }
            else
            {
                sLocation = context.SLocation;
                jsonHelper = new JsonHelper();
            }


            JToken currentContainer = jsonHelper.GetContent(container, sLocation);
            jsonHelper.BuildJObj(target, context.TLocation);

            JToken currentTarget = jsonHelper.GetLoacation(target, context.TLocation);

            jsonHelper.AddData(currentContainer, currentTarget, context.MappingTerms);

            if (context.Context != null && context.Context.Count != 0)
            {
                foreach (MappingContext item in context.Context)
                    PerfTarget(currentContainer, currentTarget, item);
            }

            MaxRank = 20;
        }

    }
}
