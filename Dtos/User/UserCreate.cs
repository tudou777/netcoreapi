
using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Dtos.User
{
    /// <summary>
    ///系统用户创建类
    /// </summary>
    public class UserCreate
    {

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
        /// 组织机构id
        /// </summary>
        public string user_org_id { get; set; }
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
        /// 角色id
        /// </summary>
        public string user_role_id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string user_memo { get; set; }

        /// <summary>
        /// 报警方式，（ 报警 微信,短信,语音,邮件。这些选项，用‘,'逗号分隔 ）
        /// </summary>
        public List<string> user_offline_alert_method { get; set; } = new List<string>();

        /// <summary>
        /// 是否接收离线报警
        /// </summary>
        public int user_receive_offline_alert { get; set; } = 0;

        /// <summary>
        /// 用户过期时间 （默认为空，过期自动禁用)
        /// </summary>
        public DateTime? user_expire_time { get; set; }
    }
}
