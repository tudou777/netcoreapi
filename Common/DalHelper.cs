using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Common
{
    public class DalHelper
    {
        /// <summary>
        /// 获取分页语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderRule">排序规则</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="pageNo">页码</param>
        /// <returns>分页查询语句</returns>
        public static string GetPagerSql(string tableName, string fields = "*", string orderRule = " ", string condition = "", int pageSize = 10, int pageNo = 1)
        {
            string sql = "select {0} from {1} where 1=1 {2} order by {3} limit  " + pageSize + "  offset  " + (pageNo - 1) * pageSize + " ";
            return String.Format(sql, fields, tableName, condition, orderRule);
        }
    }
}
