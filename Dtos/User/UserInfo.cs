using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Dtos.User
{
    /// <summary>
    /// 用户展示类
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户id （guid类型）
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 用户账户
        /// </summary>
        public string user_account { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string user_phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string user_email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string user_wxid { get; set; }
        /// <summary>
        /// 系统id
        /// </summary>
        public string user_system_id { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string system_name { get; set; }
    }
}
