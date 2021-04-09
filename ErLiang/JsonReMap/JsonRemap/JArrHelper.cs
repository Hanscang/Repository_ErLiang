using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ErLiang.JsonReMap
{

    internal class JArrHelper : JHelper
    {
        public override void AddData(JToken container, JToken target, List<JsonMappingTerm> mappingTerms)
        {
            if (container == null || target == null || mappingTerms == null || mappingTerms.Count == 0) return;

            if (!(container is JArray))
                throw new Exception($"尝试以{nameof(JArray)}方式读取一个{nameof(container)}");
            if (!(target is JArray))
                throw new Exception($"尝试以{nameof(JArray)}方式读取一个{nameof(target)}");

            JArray jArray = (JArray)container;
            JArray jTarget = (JArray)target;
            foreach (JToken sourceData in jArray)
            {
                JObject temp = new JObject();
                foreach (JsonMappingTerm mapping in mappingTerms)
                {
                    if (sourceData[mapping.Source] == null) continue;

                    temp.Add(mapping.Target, sourceData[mapping.Source]);
                }
                jTarget.Add(temp);
            }
        }

        protected override void AddChild(JObject target, string name)
        {
            if (target == null) return;

            if (target[name] == null)
                target.Add(name, new JArray());
        }
    }
}
