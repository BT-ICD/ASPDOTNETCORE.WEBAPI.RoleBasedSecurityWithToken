using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore3JWT.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

/// <summary>
/// To learn about SignarR with ASP.NET CORE Web API
/// Reference:
/// https://www.roundthecode.com/dotnet/asp-net-core-web-api/using-signalr-in-asp-net-core-react-to-send-messages
/// 
/// </summary>
namespace AspNetCore3JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        protected readonly IHubContext<MessageHub> messageHub;
        public MessageController(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
        }
        [HttpPost]
        public async Task<IActionResult> Create(MessagePost messagePost)
        {
            await messageHub.Clients.All.SendAsync("sentToClient", "The message " + messagePost.Message + " has been received");
            return Ok();
        }
    }

    public class MessagePost
    {
        public string Message { get; set; }
    }
}