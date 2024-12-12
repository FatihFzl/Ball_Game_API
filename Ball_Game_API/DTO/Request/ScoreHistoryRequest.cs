namespace Ball_Game_API.DTO.Request
{
    public class ScoreHistoryRequest
    {
        public required int Player1Score { get; set; }
        public required int Player2Score { get; set; }
        public required string Player1Name { get; set; }
        public required string Player2Name { get; set; }
    }
}
