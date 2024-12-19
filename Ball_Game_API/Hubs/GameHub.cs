using Ball_Game_API.Ball;
using Ball_Game_API.GameCharacters;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
namespace Ball_Game_API.Hubs
{
    public class GameHub : Hub
    {

        private readonly GameCharactersManager _gameCharactersManager;
        private readonly GameRunnerBackgroundJob _gameRunnerBackgroundJob;

        public GameHub(GameCharactersManager gameCharactersManager, GameRunnerBackgroundJob gameRunnerBackgroundJob)
        {
            _gameCharactersManager = gameCharactersManager;
            _gameRunnerBackgroundJob = gameRunnerBackgroundJob;
        }

        public async Task JoinGame(string userName,string groupId)
        {
            var conCount = _gameCharactersManager.GetCharacterCount();

            if(conCount >= 2)
            {
                await Clients.Caller.SendAsync("UnableToJoinListener");
            };
             
            var character = _gameCharactersManager.GetOrCreateCharacter(Context.ConnectionId, userName);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

            await Clients.Group(groupId).SendAsync("JoinGameReciever", character.Username, _gameCharactersManager.GetCharacterCount());
        }

        public async Task StartGame(string groupId)
        {
            _gameRunnerBackgroundJob.StartJob();
            await Clients.Group(groupId).SendAsync("StartGameReciever");
        }

        public async Task BallRunner(string groupId)
        {
            var characters = _gameCharactersManager.GetCharacters();

            GameBall.MoveBall();
            
            foreach(var character in characters)
            {
                character.CheckIfBallBounced();
            }

            var ballPosition = GameBall.GetBallPosition();

            if(ballPosition.BallPositionY <= -5)
            {
                //SCORE
                await ResetRound(groupId);
            }

            if(ballPosition.BallPositionY >= 655)
            {
                //SCORE
               await ResetRound(groupId);
            }

            await Clients.Group(groupId).SendAsync("BallReciever", GameBall.GetBallPosition());

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
            _gameRunnerBackgroundJob.StopJob();
            await Clients.All.SendAsync("ResetGameReciever");
        }


        public async Task ResetRound(string groupId)
        {
            GameBall.ResetBall();
            await Clients.Group(groupId).SendAsync("BallReciever", GameBall.GetBallPosition());

            _gameRunnerBackgroundJob.StopJob();

            await Task.Delay(3000);

            _gameRunnerBackgroundJob.StartJob();

        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _gameCharactersManager.RemoveCharacter(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

    }




}
