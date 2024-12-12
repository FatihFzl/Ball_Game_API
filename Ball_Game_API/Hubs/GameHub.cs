using Ball_Game_API.Ball;
using Ball_Game_API.DTO.Socket;
using Microsoft.AspNetCore.SignalR;

namespace Ball_Game_API.Hubs
{
    public class GameHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }
        public async Task Ping(string text)
        {

            await Clients.All.SendAsync("Pong", text);
        }

        public async Task GameRunner()
        {
            GameBall.MoveBall();
            
            await Clients.All.SendAsync("GameRun", GameBall.GetBallPosition());
        }

        public async Task ResetRound()
        {
            GameBall.ResetBall();

            await Clients.All.SendAsync("GameRun", GameBall.GetBallPosition());
        }

    }




}
