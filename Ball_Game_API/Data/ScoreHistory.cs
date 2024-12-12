namespace Ball_Game_API.Data
{
    public class ScoreHistory
    {
        public int Id { get; set; }
        public int Player1Score{ get; set; }
        public int Player2Score{ get; set; }
        public required string Player1Name { get; set; }
        public required string Player2Name { get; set; }
        public DateTime GameDate { get; set; }
    }
}