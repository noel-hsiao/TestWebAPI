using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {        
        [HttpPost("Create")]
        public void CreateAccount([FromBody] JsonElement value)
        {
            JObject data = JObject.Parse(value.GetRawText());            
            Account account = new Account();
            if(account.CreateAccount(data["UserName"].ToString(), data["Password"].ToString()))
            {
                Ok();
            }
            else
            {
                Problem();
            }
        }

        [HttpPost("Login")]
        public void Login([FromBody] JsonElement value)
        {
            JObject data = JObject.Parse(value.GetRawText());
            Account account = new Account();
            if (account.Login(data["UserName"].ToString(), data["Password"].ToString()))
            {
                Ok();
            }
            else
            {
                Problem();
            }
        }
    }
}
