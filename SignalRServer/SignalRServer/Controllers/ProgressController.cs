using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRServer.Hubs;

namespace SignalRServer.Controllers
{
    [ApiController]
    [Route("api/progress")]
    public class ProgressController : Controller
    {
        private readonly ILogger<ProgressController> _logger;
        private readonly IHubContext<ProgressHub> _progressHubContext;

        public ProgressController(ILogger<ProgressController> logger, IHubContext<ProgressHub> progressHubContext)
        {
            _logger = logger;
            _progressHubContext = progressHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Up(Dictionary<string,string> data)
        {
            Dictionary<string,string> outData = new Dictionary<string,string>();
            int iProgress = 0;
            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(500);
                iProgress = i * 10;
                await _progressHubContext.Clients.All.SendAsync("UpdProgress", iProgress);
            }
            outData["ResultMsg"] = "上傳圖片完成,正在處理中";
            outData["status"] = "200";
            //return Ok(outData);
            return Json(outData);
        }

        [HttpGet]
        public async Task<IActionResult> t1()
        {
            int iProgress = 0;
            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(1000);
                iProgress=i*10;
                await _progressHubContext.Clients.All.SendAsync("HandlerProgress", iProgress);
            }
            return Accepted(1);
        }
    }
}
