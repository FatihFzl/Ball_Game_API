using Ball_Game_API.Ball;
using Ball_Game_API.DTO.Socket;
using Microsoft.AspNetCore.SignalR;
using Ball_Game_API.GameCharacters;
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

        public async Task BallRunner()
        {
            GameBall.MoveBall();
            
            await Clients.All.SendAsync("BallReciever", GameBall.GetBallPosition());

        }

        public async Task MoveCharacter1Right()
        {
            Characters.MoveBar1Right();

            await Clients.All.SendAsync("Character1Reciever", Characters.GetBar1Position());
        }
        
        public async Task MoveCharacter1Left()
        {
            Characters.MoveBar1Left();
            await Clients.All.SendAsync("Character1Reciever", Characters.GetBar1Position());
        }

        public async Task ResetRound()
        {
            GameBall.ResetBall();
            Characters.ResetBar();

            await Clients.All.SendAsync("BallReciever", GameBall.GetBallPosition());
            await Clients.All.SendAsync("CharacterReciever", Characters.GetBar1Position());

        }

    }




}
