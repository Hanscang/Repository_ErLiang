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
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="container">数据容器</param>
        /// <param name="target">添加目标</param>
        /// <param name="mappingTerms">添加项</param>
        void AddData(JToken container, JToken target, List<JsonMappingTerm> mappingTerms);
        /// <summary>
        /// 构建目标路径，使目标存在相应的位置来存放数据
        /// </summary>
        /// <param name="target">当前位置</param>
        /// <param name="targetStr">相对路径</param>
        void BuildJObj(JToken target, string targetStr);
        /// <summary>
        /// 获取目标位置的引用
        /// </summary>
        /// <param name="container">当前位置</param>
        /// <param name="location">相对路径</param>
        /// <returns></returns>
        JToken GetLoacation(JToken container, string location);
        /// <summary>
        /// 获取目标位置的数据
        /// </summary>
        /// <param name="container">当前位置</param>
        /// <param name="location">相对路径</param>
        /// <returns></returns>
        JToken GetContent(JToken container, string location);
    }
}
