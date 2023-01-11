using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{/// <summary>
 /// 登陆信息
 /// </summary>
 /// <param name="loginReq">请求参数</param>
 /// <returns></returns>
    [HttpPost]
    public ActionResult<LoginResult> Login(LoginRequest loginReq)
    {
        if (loginReq.UserName == "admin" && loginReq.Password == "123456")
        {
            var processes = Process.GetProcesses().Select(p => new ProcessInfo(
                p.Id, p.ProcessName, p.WorkingSet64)).ToArray();
            return new LoginResult(true, processes);
        }
        else
        {
            return new LoginResult(false, null);
        }
    }
}