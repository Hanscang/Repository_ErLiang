using Newtonsoft.Json.Linq;

namespace ErLiang.JsonReMap
{

    internal class JObjHelper : JHelper
    {
        public override JToken GetContent(JToken container, string location)
        {
            if (container == null || string.IsNullOrEmpty(location)) return container;
            JToken targetData = base.GetContent(container, location);
            JToken result = JObject.Parse(targetData.ToString());
            return result;
        }

    }
}
