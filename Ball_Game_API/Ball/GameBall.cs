using Ball_Game_API.DTO.Socket;
using Ball_Game_API.GameCharacters;

namespace Ball_Game_API.Ball
{
    public static class GameBall
    {

        private readonly static int ballSpeed = 3;
        private static int directionY = 1;
        private static int directionX = 1;
        private static int ballPositionX = 650;
        private static int ballPositionY = 350;

        public static int BallPositionX
        {
            get { return ballPositionX; }
            private set { ballPositionX = value; }
        }

        public static int BallPositionY
        {
            get { return ballPositionY; }
            private set { ballPositionY = value; }
        }

        public static void MoveBall()
        {
            if(BallPositionX + 20 >= 1300 || BallPositionX <= 0)
            {
                directionX *= -1;
            }

            BallPositionX += ballSpeed * directionX;
            BallPositionY += ballSpeed * directionY;

            if (ballPositionY > 600)
            {
                if (ballPositionX >= Characters.Bar1PositionX && ballPositionX <= Characters.Bar1PositionX + 30)
                {
                    directionX = directionX * 0;
                    directionY = directionY * -1;
                    
                }

                // orta kısımlar
                else if (ballPositionX > Characters.Bar1PositionX + 100 && ballPositionX <= Characters.Bar1PositionX + 100)
                {
                    directionX = directionX * 0;
                    directionY = directionY * -1;
                }

                // sağ parça
                else if (ballPositionX > Characters.Bar1PositionX + 30 && ballPositionX <= Characters.Bar1PositionX + 160)
                {
                    directionX = directionX * -1;
                    directionY = directionY * -1;
                }
            }
        }

        public static BallMovementSocketDTO GetBallPosition()
        {
            return new BallMovementSocketDTO
            {
                BallPositionX = BallPositionX,
                BallPositionY = BallPositionY
            };
        }

        public static void ResetBall()
        {
            BallPositionX = 650;
            BallPositionY = 350;
            RandomizeDirections();
        }

        private static void RandomizeDirections()
        {
            Random random = new();

            directionX = random.Next(0, 2) == 0 ? 1 : -1;

            directionY = random.Next(0, 2) == 0 ? 1 : -1;
        }

    }
}
