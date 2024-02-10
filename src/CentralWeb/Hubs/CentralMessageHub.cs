using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Services.Dtos.Central;

namespace CentralWebInHospital.Hubs
{
    [Authorize]
    public class CentralMessageHub : Hub
    {
        private List<KeyValuePair<int, int>> _onlineUsersCentrals  =  new List<KeyValuePair<int, int>>();

        public async Task SendMessage(CentralMessageDto message)
        {
            await Clients.All.SendAsync("Send", message);
        }

        public void RegisterUserWithDesiredCentral(int centralId) => 
            this._onlineUsersCentrals.Add(new KeyValuePair<int, int>(int.Parse(this.Context.User?.Identity.GetUserId()!), centralId));

        public void GetRegisteredUsersCentrals()
        {
            var res = this._onlineUsersCentrals;
            this.Clients.Caller.SendAsync("ReceiveRegisteredUsers", res);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
