using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TD.Dtos;
using TD.Bll;
using TD.Common;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly IMapper _mapper;
        SystemBll systemBll = null;
        public SystemController(IMapper mapper)
        {
            _mapper = mapper;
            systemBll = new SystemBll(Config.GetConnectionString("DefaultConnection"), _mapper);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("token")]
        public ActionResult<string> GetToken()
        {
            return Ok(TD.Common.JwtHelper.create_Token("1", "tudou", "admin"));
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">系统对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ReturnModel> Insert(TD.Dtos.System.SystemCreate entity)
        {
            ReturnModel result = new ReturnModel();
            try
            {
                result = systemBll.Insert(entity);
                if (result.succeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<SystemController>(ex.Message);
                result.code = ReturnCode.abnormal;
                result.msg = ex.Message;
                return BadRequest(result);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">系统对象</param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult<ReturnModel> Update(TD.Dtos.System.SystemUpdate entity)
        //{
        //    ReturnModel result = new ReturnModel();
        //    try
        //    {
        //        result = systemBll.Insert(entity);
        //        if (result.succeded)
        //        {
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            return BadRequest(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error<SystemController>(ex.Message);
        //        result.code = ReturnCode.abnormal;
        //        result.msg = ex.Message;
        //        return BadRequest(result);
        //    }
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="keyword">系统名称关键字</param>
        /// <param name="pageSize">数量</param>
        /// <param name="pageNo">页码</param>
        /// <returns></returns>

        [HttpGet]
        [Route("pager")]
        public ActionResult<PagedResult<TD.Dtos.System.SystemList>> GetPager(string keyword = "", int pageSize = 10, int pageNo = 1)
        {
            try
            {
                List<TD.Dtos.System.SystemList> list = systemBll.GetPager(keyword, pageSize, pageNo);
                int count = systemBll.GetCount(keyword);
                LogHelper.Error<SystemController>("查询系统列表");
                return new PagedResult<TD.Dtos.System.SystemList>(list, pageNo, pageSize, count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}