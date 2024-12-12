using Ball_Game_API.Data;
using Ball_Game_API.DTO.Request;
using Ball_Game_API.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace Ball_Game_API.Services
{
    public class ScoreHistoryService : IScoreHistoryService
    {

        private readonly AppDbContext _dbContext;

        public ScoreHistoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ScoreHistory>> GetScoreHistoriesAsync()
        {
            return await _dbContext.ScoreHistories.ToListAsync();
        }

        public async Task<ScoreHistoryResponse> InsertScoreHistoryAsync(ScoreHistoryRequest scoreHistoryRequest)
        {
            var newScoreHistory = new ScoreHistory
            {
                Player1Name = scoreHistoryRequest.Player1Name,
                Player2Name = scoreHistoryRequest.Player2Name,
                Player1Score = scoreHistoryRequest.Player1Score,
                Player2Score = scoreHistoryRequest.Player2Score,
                GameDate = DateTime.UtcNow
            };

              _dbContext.ScoreHistories.Add(newScoreHistory);
            await _dbContext.SaveChangesAsync();
            return new ScoreHistoryResponse
            {
                Player1Name = newScoreHistory.Player1Name,
                Player2Name = newScoreHistory.Player2Name,
                Player1Score = newScoreHistory.Player1Score,
                Player2Score = newScoreHistory.Player2Score,
                GameDate = newScoreHistory.GameDate,
                Id = newScoreHistory.Id
                
            };
        }
    }
}
