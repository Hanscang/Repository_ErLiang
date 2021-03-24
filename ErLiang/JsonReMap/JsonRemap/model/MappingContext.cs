using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErLiang.JsonReMap
{
    /// <summary>
    /// 映射上下文
    /// </summary>
    /// <remarks>
    /// 为解决json中存在的序列化为string的对象的反序列化问题，以及数组的操作差异问题
    /// 引入上下文概念
    /// </remarks>
    [Serializable]
    public class MappingContext
    {
        /// <summary>
        /// source路径
        /// .为分隔符
        /// 后缀为Mappingsymbol，以便实现特定操作
        /// </summary>
        public string SLocation { get; set; }
        /// <summary>
        /// target路径
        /// .为分隔符
        /// 后缀为Mappingsymbol，以便实现特定操作
        /// </summary>
        public string TLocation { get; set; }


        /// <summary>
        /// 具体的映射项
        /// .为分隔符
        /// </summary>
        public List<JsonMappingTerm> MappingTerms { get; set; }
        /// <summary>
        /// 子级上下文
        /// </summary>
        public List<MappingContext> Context { get; set; }
    }
    public class MappingSymbol
    {
        /// <summary>
        /// 数组标识
        /// </summary>
        public static string ARR { get; set; } = "[]";
        /// <summary>
        /// 以Json的方式储存在对象当中
        /// </summary>
        public static string ToJson { get; set; } = "{}";
        public const char Separator = '.';
    }
}
