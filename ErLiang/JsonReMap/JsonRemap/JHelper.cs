using ErLiang.JsonReMap;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErLiang.JsonReMap
{
    internal abstract class JHelper : IJHelper
    {
        public virtual void AddChild(JObject target, string name)
        {
            if (target == null) return;

            if (target[name] == null)
                target.Add(name, new JObject());
        }
        public virtual void AddData(JToken container, JToken target, List<JsonMappingTerm> mappingTerms)
        {
            if (container == null || target == null || mappingTerms == null || mappingTerms.Count == 0) return;
            foreach (JsonMappingTerm item in mappingTerms)
            {
                if (!(container is JObject))
                    throw new Exception($"尝试以{nameof(JObject)}方式读取一个{nameof(container)}, {nameof(item.Source)}:{item.Source}");

                if (container[item.Source] == null) continue;


                ((JObject)target).Add(item.Target, container[item.Source]);
            }
        }
        public virtual void BuildJObj(JToken target, string targetStr)
        {
            if (string.IsNullOrEmpty(targetStr) || target == null) return;

            string[] locationLs = targetStr.Split(MappingSymbol.Separator);

            BuildJObj((JObject)target, locationLs);
        }
        private void BuildJObj(JObject target, string[] locationLs)
        {
            string lastName = string.Empty;
            if (locationLs == null || locationLs.Length == 0) return;

            lastName = locationLs.Last();
            if (lastName.EndsWith(MappingSymbol.ToJson))
                lastName = lastName.Substring(0, lastName.Length - 2);

            for (int i = 0; i < locationLs.Length - 1; i++)
            {
                JToken temp = target[locationLs[i]];
                if (temp == null)
                {
                    target.Add(locationLs[i], new JObject());
                }
                else
                {
                    target = (JObject)temp;
                }
            }

            AddChild(target, lastName);
        }

        public virtual JToken GetContent(JToken container, string location)
        {
            return GetLoacation(container, location);
        }
        public virtual JToken GetLoacation(JToken container, string location)
        {
            if (container == null) return container;
            if (string.IsNullOrEmpty(location)) return container;

            if (location.EndsWith(MappingSymbol.ToJson) || location.EndsWith(MappingSymbol.ARR))
                location = location.Substring(0, location.Length - 2);

            string[] locationLs = location.Split(MappingSymbol.Separator);
            if (location.Length == 0) return container;

            JToken result = container;
            foreach (string name in locationLs)
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("路径名错误");

                result = result[name];

                if (result == null) return null;
            }
            return result;
        }
    }
}
