using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using TD.Model;
using TD.Common;

namespace TD.Dal
{
    
    /// <summary>
    /// 用户数据操作类
    /// </summary>
    public class UserDal
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string ConnectionString;
        public UserDal(string connStr)
        {
            this.ConnectionString = connStr;
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity">系统数据</param>
        public void Insert(UserModel entity)
        {
            using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                conn.Insert(entity);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">系统数据</param>
        public void Update(UserModel entity)
        {
            using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                conn.Update(entity);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public void Delete(string user_id)
        {
            string sql = "delete from t_user where user_id=@user_id";
            using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                conn.Execute(sql, new { user_id = user_id });
            }
        }
        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="user_id">用户id</param>
        /// <returns></returns>
        public SystemModel Get(string user_id)
        {
            string sql = "select * from t_user where user_id=@user_id";
            using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                return conn.QueryFirstOrDefault<SystemModel>(sql, new { user_id = user_id });
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<UserModel> GetList(string keyword)
        {
            string sql = "select * from t_user where 1=1 " + GetCondition(keyword);
            using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                return conn.Query<UserModel>(sql, new { keyword = "%" + keyword + "%" }).AsList();
            }
        }
       /// <summary>
       /// 获取分页数据
       /// </summary>
       /// <param name="keyword"></param>
       /// <param name="pageSize"></param>
       /// <param name="pageNo"></param>
       /// <returns></returns>
        public List<UserModel> GetPager(string keyword, int pageSize, int pageNo)
        {
            string sql = DalHelper.GetPagerSql("t_user", "*", " user_name asc", GetCondition(keyword), pageSize, pageNo);
            using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {

                return conn.Query<UserModel>(sql, new { keyword = "%" + keyword + "%" }).AsList();

            }
        }
        /// <summary>
        /// 获取数量（配合分页使用）
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int GetCount(string keyword)
        {
            string sql = "select count(1) from t_user where 1=1 " + GetCondition(keyword);
            using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                return Convert.ToInt32(conn.ExecuteScalar(sql, new { keyword = "%" + keyword + "%" }));
            }
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public string GetCondition(string keyword)
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                result.Append(" and user_name like @keyword");
            }
            return result.ToString();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <returns>验证成功返回空字符串；验证失败返回失败原因描述</returns>
        public string Validation(UserModel model, DbOperType ot)
        {
            StringBuilder errorMsg = new StringBuilder();
            if(ot== DbOperType.Update)
            {
                if (string.IsNullOrWhiteSpace(model.user_id))
                {
                    errorMsg.Append("账号id不能为空,");
                }
            }
            
            if (string.IsNullOrWhiteSpace(model.user_account))
            {
                errorMsg.Append("用户账号不能为空,");
            }
            string tmp = errorMsg.ToString();
            return tmp.Length > 0 ? tmp.Substring(0, tmp.Length - 1) : tmp;
        }
        /// <summary>
        /// 保存时判断数据是否存在
        /// </summary>
        /// <param name="system_name">系统名称</param>
        /// <param name="system_id">系统id（新增传入空字符串；修改传入id）</param>
        /// <returns>存在返回true；不存在返回false</returns>
        public bool IsExists(string system_name, string system_id = "")
        {
            string sql = "";
            if (string.IsNullOrWhiteSpace(system_id))//添加判断
            {
                sql = "select count(1) from t_system where system_id=@system_id and system_name=@system_name ";
                using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
                {
                    object obj = conn.ExecuteScalar(sql, new { system_id = system_id, system_name = system_name });
                    if (obj != null)
                    {
                        return Convert.ToInt32(obj) > 0 ? true : false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else//修改判断
            {
                sql = "select system_id from t_system where system_name=@system_name and system_id<>@system_id";
                using (var conn = new Npgsql.NpgsqlConnection(ConnectionString))
                {
                    object obj = conn.ExecuteScalar(sql, new { system_id = system_id, system_name = system_name });

                    if (obj == null)
                    {
                        return false;
                    }
                    else
                    {
                        string id = obj.ToString();
                        if (system_id.Equals(id))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
            }

        }


    }
}
