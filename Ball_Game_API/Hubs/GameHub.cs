using Ball_Game_API.DTO.Socket;
using Microsoft.AspNetCore.SignalR;

namespace Ball_Game_API.Hubs
{
    public class GameHub: Hub
    {

        public override async Task OnConnectedAsync() { 
            Console.WriteLine($"Client connected: {Context.ConnectionId}"); 
            await base.OnConnectedAsync(); 
        }
        public async Task Ping(string text) {

             await Clients.All.SendAsync("Pong",text);
        }

        //public async Task BallPosition(int ballPositionX, int ballPositionY)
       // {
         //   await Clients.All.SendAsync("Ball", ballPositionX,ballPositionY);
      //  }

        public async Task Ball(BallMovementSocketDTO ball)
        {
            await Clients.All.SendAsync("top", ball);
        }
    }
}
