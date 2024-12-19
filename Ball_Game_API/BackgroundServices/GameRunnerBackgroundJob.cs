using Ball_Game_API.GameCharacters;
using Ball_Game_API.Hubs;
using Microsoft.AspNetCore.SignalR;

public class GameRunnerBackgroundJob : BackgroundService
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly GameCharactersManager _gameCharactersManager;

    private readonly int _intervalInMilliseconds;
    private bool _isRunning;

    public GameRunnerBackgroundJob(IHubContext<GameHub> hubContext, GameCharactersManager gameCharactersManager)
    {
        _hubContext = hubContext;
        _gameCharactersManager = gameCharactersManager;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_isRunning)
            {
                try
                {
                    await _hubContext.Clients.Client(_gameCharactersManager.GetCharacterKeys()[0]).SendAsync("BallRunnerClientReciever");

                    await Task.Delay(16, stoppingToken);
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Job encountered an error: {ex.Message}");
                }
            }
            else
            {
                await Task.Delay(100, stoppingToken); // Bekleme süresi
            }
        }
    }

    public void StartJob()
    {
        if (_isRunning)
        {
            Console.WriteLine("Job is already running.");
            return;
        }

        _isRunning = true;
        Console.WriteLine("Job started.");
    }

    public void StopJob()
    {
        if (!_isRunning)
        {
            Console.WriteLine("Job is not running.");
            return;
        }

        _isRunning = false;
        Console.WriteLine("Job stopped.");
    }
}
