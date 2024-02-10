using System.Text;
using System.Text.Json;
using CentralWebInHospital.Hubs;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.Dtos.Central;
using Services.Services.CentralMessage;
using Services.Services.Signal;

namespace CentralWebInHospital.Controllers.Central
{
    [Route("api/central/parameter")]
    [ApiController]
    public class CentralMessageController : ControllerBase
    {
        private readonly IHubContext<CentralMessageHub> _centralMessageHubContext;
        private readonly ICentralMessageService _centralMessageService;
        private readonly IHistoryService _historyService;

        public CentralMessageController(IHubContext<CentralMessageHub> centralMessageHubContext, ICentralMessageService centralMessageService, IHistoryService signalService)
        {
            this._centralMessageHubContext = centralMessageHubContext;
            this._centralMessageService = centralMessageService;
            this._historyService = signalService;
        }

        [HttpPost]
        [Authorize(Roles = "Central")]
        public async Task<IActionResult> Post([FromBody] CentralMessageDto requestData, CancellationToken cancellationToken)
        {
            if (requestData == null) return BadRequest();
            var centralId = int.Parse(User.Identity.GetUserId());
            await this._historyService.InsertReceivedData(centralId, requestData, cancellationToken);
            //await this.handlePostToAnotherOrigin(requestData);
            var userIds = await this._centralMessageService.FindCentralMessageRelatedToSpecificUsers(centralId, requestData.Beds.Count, cancellationToken);
            var alarmsWithMessage = await this._centralMessageService.GetAlarmsWithMessage(requestData);
            await this._centralMessageHubContext.Clients.Users(userIds).SendAsync("Send", alarmsWithMessage, cancellationToken);
            //await this.handlePostToAnotherOrigin2(requestData);
            return Ok();
        }

        [HttpGet("get-desired-central/{centralId:int}")]
        [Authorize]
        public async Task<IActionResult> GetDesiredCentral(int centralId, CancellationToken cancellationToken)
        {
            var userId = int.Parse(this.User.Identity.GetUserId());
            this._centralMessageService.GetDesiredCentral(userId, centralId);
            return Ok("Done!");
        }

        [HttpGet("release-desired-central/{centralId:int}")]
        [Authorize]
        public async Task<IActionResult> ReleaseDesiredCentral(int centralId, CancellationToken cancellationToken)
        {
            var userId = int.Parse(this.User.Identity.GetUserId());
            this._centralMessageService.ReleaseDesiredCentral(userId, centralId);
            return Ok("Done!");
        }

        // for test 😂
        private async Task handlePostToAnotherOrigin(CentralMessageDto requestData)
        {
            const string apiUrl = "https://cweb.recase.ir/api/central/parameter";
            var jsonPayload = JsonSerializer.Serialize(requestData);
            var client = new HttpClient();
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
        }

        private async Task handlePostToAnotherOrigin2(CentralMessageDto requestData)
        {
            const string apiUrl = "http://192.168.4.68:5000/api/central/parameter";
            var jsonPayload = JsonSerializer.Serialize(requestData);
            var client = new HttpClient();
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
        }
    }
}