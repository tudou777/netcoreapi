using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Dtos
{
    /// <summary>
    /// 返回分页数据类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        public class PagingInfo
        {
            /// <summary>
            /// 页码
            /// </summary>
            public int pageNo { get; set; }
            /// <summary>
            /// 每页条数
            /// </summary>
            public int pageSize { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int pageCount { get; set; }
            /// <summary>
            /// 总条数
            /// </summary>
            public long totalRecordCount { get; set; }

        }
        public List<T> Data { get; private set; }

        public PagingInfo Paging { get; private set; }

        public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Data = new List<T>(items);
            Paging = new PagingInfo
            {
                pageNo = pageNo,
                pageSize = pageSize,
                totalRecordCount = totalRecordCount,
                pageCount = totalRecordCount > 0
                    ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                    : 0
            };
        }
    }
}
