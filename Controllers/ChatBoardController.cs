using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestWebAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBoardController : ControllerBase
    {
        private static ChatBoard chatBoard = new ChatBoard();
        
        [HttpGet("{quantity}")]
        public string Get(int quantity)
        {
            var chats = chatBoard.GetChats(quantity);
            
            return JsonSerializer.Serialize(chats);
        }

        [HttpPost]
        public void Post([FromBody] JsonElement value)
        {
            JObject data = JObject.Parse(value.GetRawText());

            if (chatBoard.CreateChat(data["CreateUserName"].ToString(), data["Title"].ToString(), data["ContentText"].ToString()))
            {
                Ok();
            }
            else
            {
                Problem();
            }
        }

        // PUT api/<ChatBoardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JsonElement value)              
        {
            JObject data = JObject.Parse(value.GetRawText());
            if (chatBoard.EditChat(id, data["ContentText"].ToString()))
            {
                Ok();
            }
            else
            {
                Problem();
            }
        }

        // DELETE api/<ChatBoardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (chatBoard.DeleteChat(id))
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
