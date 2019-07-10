using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRHub;
using System;
using System.Net.Http;
using System.Text;

namespace SignalR_Hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public MessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public string Post([FromBody]Message msg)
        {
            string retMessage;

            try
            {
                var client = new HttpClient();
                client.PostAsync("https://5lqbx47uxa.execute-api.us-east-1.amazonaws.com/dev/messages", new StringContent(msg.Payload, Encoding.UTF8, "application/json"));

                _hubContext.Clients.All.BroadcastMessage(msg.Type, msg.Payload);


                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}