using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NCApi.Extensions;

namespace NCApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IApiTokenService _apiTokenService;
        public AccountController(IApiTokenService apiTokenService)
        {
            _apiTokenService = apiTokenService;
        }
        
        [HttpPost]
        public IActionResult Login(string userName, string userPwd)
        {
            //login...
            if (userName != "admin" && userPwd != "123456")
                return Ok(new { status = 0, msg = "Login failed" });
            var userId = 1;
            var token = _apiTokenService.ConvertLoginToken(userId, userName);
            //登录成功后返回token
            return Ok(new { status = 1, msg = "Login success", data = token });
        }

    }
}

