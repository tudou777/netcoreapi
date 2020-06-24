using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TD.Common;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult Test()
        {
            //List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            //int sum = TD.Common.Test.Cul<int, int>(list, TD.Common.Test.Sum);
            //return Ok(sum.ToString());
            //CC cc = new CC();
            //Dog dog = new Dog();
            //return Ok(cc.show(dog));
            int? num = GetNullableType();
            return Ok(num ?? 1);
        }

        private int? GetNullableType()
        {
            return null;
        }
    }
}