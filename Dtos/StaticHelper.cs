using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Dtos
{
   public class StaticHelper
    {

    }
    /// <summary>
    /// 返回码
    /// </summary>
    public class ReturnCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static string success { get; } = "S0001";
        /// <summary>
        /// 失败
        /// </summary>
        public static string fail { get; } = "F0001";
        /// <summary>
        /// 找不到对象
        /// </summary>
        public static string not_found { get; } = "E0001";
        /// <summary>
        /// 数据为空
        /// </summary>
        public static string is_null { get; } = "E0002";
        /// <summary>
        /// 数据重复
        /// </summary>
        public static string is_repeat { get; } = "E0003";
        /// <summary>
        /// 没有权限
        /// </summary>
        public static string no_permission { get; } = "E0004";
        /// <summary>
        /// 异常
        /// </summary>
        public static string abnormal { get; } = "E0005";
    }
}
