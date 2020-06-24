using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TD.Model;
using TD.Dtos;
using TD.Dtos.System;
using TD.Common;
using TD.Dal;
using AutoMapper;

namespace TD.Bll
{
    public class SystemBll
    {
        private IMapper _mapper;
        private string _connectionString;
        SystemDal systemDal;
        public SystemBll(string connStr, IMapper mapper)
        {
            this._mapper = mapper;
            this._connectionString = connStr;
            systemDal = new SystemDal(connStr);
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity">创建类</param>
        public ReturnModel Insert(SystemCreate entity)
        {
            //结果对象
            ReturnModel returnModel = new ReturnModel();
            try
            {
                //生成对象
                SystemModel model = _mapper.Map<SystemModel>(entity);
                //非空验证
                returnModel.msg = systemDal.Validation(model,DbOperType.Insert);
                if (!string.IsNullOrWhiteSpace(returnModel.msg))
                {
                    returnModel.code = ReturnCode.is_null;
                    return returnModel;
                }
                //重复验证
                if (systemDal.IsExists(model.system_name))
                {
                    returnModel.code = ReturnCode.is_repeat;
                    returnModel.msg = "系统名称已经存在";
                    return returnModel;
                }

                model.system_id = Guid.NewGuid().ToString();
                systemDal.Insert(model);
                returnModel.succeded = true;
                returnModel.data = model.system_id;
                returnModel.code = ReturnCode.success;
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.code = ReturnCode.abnormal;
                returnModel.msg = ex.Message;
                return returnModel;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">修改类</param>
        /// <returns></returns>
        public ReturnModel Update(SystemUpdate entity)
        {
            //结果对象
            ReturnModel returnModel = new ReturnModel();
            try
            {
                //生成对象
                SystemModel model = _mapper.Map<SystemModel>(entity);
                //非空验证
                returnModel.msg = systemDal.Validation(model, DbOperType.Insert);
                if (!string.IsNullOrWhiteSpace(returnModel.msg))
                {
                    returnModel.code = ReturnCode.is_null;
                    return returnModel;
                }
                //重复验证
                if (systemDal.IsExists(model.system_name))
                {
                    returnModel.code = ReturnCode.is_repeat;
                    returnModel.msg = "系统名称已经存在";
                    return returnModel;
                }

                model.system_id = Guid.NewGuid().ToString();
                systemDal.Insert(model);
                returnModel.succeded = true;
                returnModel.data = model.system_id;
                returnModel.code = ReturnCode.success;
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.code = ReturnCode.abnormal;
                returnModel.msg = ex.Message;
                return returnModel;
            }
        }
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="system_id"></param>
        ///// <returns></returns>
        //public int Delete(string system_id)
        //{
        //    return systemDal.Delete(system_id);
        //}
        ///// <summary>
        ///// 获取一条数据
        ///// </summary>
        ///// <param name="system_id"></param>
        ///// <returns></returns>
        //public SystemModel Get(string system_id)
        //{
        //    return systemDal.Get(system_id);
        //}
        ///// <summary>
        ///// 获取数据列表
        ///// </summary>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //public List<SystemModel> GetList(string keyword)
        //{
        //    return systemDal.GetList(keyword);
        //}
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public List<SystemList> GetPager(string keyword, int pageSize, int pageNo)
        {
            List<SystemModel> source= systemDal.GetPager(keyword, pageSize, pageNo);
            List<SystemList> target = _mapper.Map<List<SystemModel>, List<SystemList>>(source);
            return target;
        }

        public int GetCount(string keyword)
        {
            return systemDal.GetCount(keyword);
        }
    }
}
