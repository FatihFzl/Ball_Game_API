using Ball_Game_API.DTO.Socket;
using Microsoft.AspNetCore.Components.Web;

namespace Ball_Game_API.GameCharacters
{
    public static class Characters
    {
        private static int bar1PositionX = 100;
        private static int step = 20;
         
        public static int Bar1PositionX {  
            get{return bar1PositionX;}
           private set { bar1PositionX = value; }
        }

        public static BarMovementSocketDTO GetBar1Position()
        {
            return new BarMovementSocketDTO
            {
                Bar1PositionX = Bar1PositionX
              
            };
        }

        public static void MoveBar1Right()
       {   
            Bar1PositionX += step;
        }

        public static void MoveBar1Left()
        {
            Bar1PositionX -= step;
        }

       
        public static void ResetBar()
        { 
            Bar1PositionX = 100;
            
        }
    }
}
