using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Dtos
{
   public class ReturnModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool succeded { get; set; } = false;
        /// <summary>
        /// 数据
        /// </summary>
        public Object data { get; set; } =new Object();
        /// <summary>
        /// 描述
        /// </summary>
        public string msg { get; set; } = "";
        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; } = ReturnCode.fail;
    }
}
