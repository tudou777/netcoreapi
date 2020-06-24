using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Model
{
    [Table("t_system")]
    public class SystemModel
    {
        /// <summary>
        /// 系统id
        /// </summary>
        [ExplicitKey]
        public string system_id { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string system_name { get; set; }
    }
}
