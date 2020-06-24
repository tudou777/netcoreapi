using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Model
{
    /// <summary>
    /// 系统用户信息表
    /// </summary>
    [Table("t_user")]
    public class UserModel
    {
        /// <summary>
        /// 用户id （guid类型）
        /// </summary>
        [ExplicitKey]
        public string user_id { get; set; }
        /// <summary>
        /// 用户账户
        /// </summary>
        public string user_account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string user_password { get; set; }

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

    }
}
