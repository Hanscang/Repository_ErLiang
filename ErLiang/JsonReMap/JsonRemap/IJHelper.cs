using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErLiang.JsonReMap;

namespace ErLiang.JsonReMap
{
    public interface IJHelper
    {
        void AddData(JToken container, JToken target, List<JsonMappingTerm> mappingTerms);
        void BuildJObj(JToken target, string targetStr);

        void AddChild(JObject target, string name);
        JToken GetLoacation(JToken container, string location);
        JToken GetContent(JToken container, string location);
    }
}
