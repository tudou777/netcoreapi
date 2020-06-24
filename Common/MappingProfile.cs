using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TD.Model;
using TD.Dtos;

namespace TD.Common
{
   public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region 系统
            //添加对象转数据库对象
            CreateMap<TD.Dtos.System.SystemCreate, SystemModel>();

            //修改对象转数据库对象
            CreateMap<TD.Dtos.System.SystemUpdate, SystemModel>();

            //数据库对象转显示对象
            CreateMap<SystemModel,TD.Dtos.System.SystemInfo>();
            CreateMap<SystemModel, TD.Dtos.System.SystemList>();
            #endregion

            #region 用户
            //添加对象转数据库对象
            CreateMap<TD.Dtos.User.UserCreate,UserModel>();

            //修改对象转数据库对象
            CreateMap<TD.Dtos.User.UserUpdate, UserModel>();

            //数据库对象转显示对象
            CreateMap<UserModel, TD.Dtos.User.UserInfo>();
            #endregion
        }
    }
}
