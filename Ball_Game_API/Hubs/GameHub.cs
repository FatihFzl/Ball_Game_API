using Ball_Game_API.Ball;
using Ball_Game_API.DTO.Socket;
using Microsoft.AspNetCore.SignalR;
using Ball_Game_API.GameCharacters;
namespace Ball_Game_API.Hubs
{
    public class GameHub : Hub
    {

        private readonly GameCharactersManager _gameCharactersManager;

        public GameHub(GameCharactersManager gameCharactersManager)
        {
            _gameCharactersManager = gameCharactersManager;
        }

        public async Task BallRunner()
        {
            var character = _gameCharactersManager.GetOrCreateCharacter(Context.ConnectionId);
            GameBall.MoveBall();
            character.CheckIfBallBounced();
            
            await Clients.All.SendAsync("BallReciever", GameBall.GetBallPosition());

        }

        public async Task MoveCharacterRight()
        {
            var character = _gameCharactersManager.GetOrCreateCharacter(Context.ConnectionId);
            character.MoveBarRight();

            await Clients.Caller.SendAsync("CharacterReciever", character.GetBarPosition());
            await Clients.AllExcept([Context.ConnectionId]).SendAsync("OpponentReciever", character.GetBarPosition());
        }
        
        public async Task MoveCharacterLeft()
        {
            var character = _gameCharactersManager.GetOrCreateCharacter(Context.ConnectionId);

            character.MoveBarLeft();
            await Clients.Caller.SendAsync("CharacterReciever", character.GetBarPosition());
            await Clients.AllExcept([Context.ConnectionId]).SendAsync("OpponentReciever", character.GetBarPosition());
        }

        public async Task InitCharacters()
        {
            var character = _gameCharactersManager.GetOrCreateCharacter(Context.ConnectionId);
            await Clients.Caller.SendAsync("CharacterReciever", character.GetBarPosition());
            await Clients.AllExcept([Context.ConnectionId]).SendAsync("OpponentReciever", character.GetBarPosition());

        }

        public async Task LeaveGame()
        {
            _gameCharactersManager.RemoveCharacter(Context.ConnectionId);
            await Clients.AllExcept([Context.ConnectionId]).SendAsync("OpponentLeft");
        }

        public async Task ResetGame()
        {
            _gameCharactersManager.ResetConnections();
            await Clients.All.SendAsync("ResetGameReciever");
        }


        public async Task ResetRound()
        {
            var character = _gameCharactersManager.GetOrCreateCharacter(Context.ConnectionId);
            GameBall.ResetBall();
            character.ResetBar();

            await Clients.All.SendAsync("BallReciever", GameBall.GetBallPosition());
            await Clients.All.SendAsync("CharacterReciever", character.GetBarPosition());

        }

    }




}
