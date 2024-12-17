using Ball_Game_API.Ball;
using Ball_Game_API.DTO.Socket;
using Microsoft.AspNetCore.Components.Web;
using System.IO;

namespace Ball_Game_API.GameCharacters
{
    public class Character
    {

        private int barPositionX = 325;
        private int barPositionY = 0;

        private int step = 20;


        public int BarPositionX
        {
            get { return barPositionX; }
            private set { barPositionX = value; }
        }

        public int BarPositionY
        {
            get { return barPositionY; }
            private set { barPositionY = value; }
        }

        public Character(int positionY) {

            barPositionY = positionY;
        }
        



        public BarMovementSocketDTO GetBarPosition()
        {
            return new BarMovementSocketDTO
            {
                BarPositionX = BarPositionX,
                BarPositionY = BarPositionY

            };
        }



        public void MoveBarRight()
        {
            BarPositionX += step;
        }

        public void MoveBarLeft()
        {
            BarPositionX -= step;
        }



        public void CheckIfBallBounced()
        {
            if (barPositionY== 0)
            {
                if (GameBall.BallPositionY <= barPositionY)
                {
                    BounceBall();
                }
            }
            else
            {
                if (GameBall.BallPositionY >= barPositionY)
                {
                    BounceBall();
                }
            }
           
        }

        public void BounceBall()
        {
            if (GameBall.BallPositionX >= BarPositionX && GameBall.BallPositionX <= BarPositionX + 30)
            {
                GameBall.directionX = GameBall.directionX * 0;
                GameBall.directionY = GameBall.directionY * -1;

            }

            // orta kısımlar
            else if (GameBall.BallPositionX > BarPositionX + 100 && GameBall.BallPositionX <= BarPositionX + 100)
            {
                GameBall.directionX = GameBall.directionX * 0;
                GameBall.directionY = GameBall.directionY * -1;
            }

            // sağ parça
            else if (GameBall.BallPositionX > BarPositionX + 30 && GameBall.BallPositionX <= BarPositionX + 160)
            {
                GameBall.directionX = GameBall.directionX * -1;
                GameBall.directionY = GameBall.directionY * -1;
            }
        }

        public void ResetBar()
        {
            BarPositionX = 500;

        }
    }
}
