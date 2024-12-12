using Ball_Game_API.Data;
using Ball_Game_API.DTO.Request;
using Ball_Game_API.DTO.Response;

namespace Ball_Game_API.Services
{
    public interface IScoreHistoryService
    {

        Task<List<ScoreHistory>> GetScoreHistoriesAsync();
        Task<ScoreHistoryResponse> InsertScoreHistoryAsync(ScoreHistoryRequest scoreHistoryRequest);
    }
}
