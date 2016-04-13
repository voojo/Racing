using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models
{
    abstract class Bonus
    {
        private int _positionX;
        private TrackYPosition _positionY;
        public void GenerateCoordinates(int lengthOfTruck)
        {
            Random random = new Random();
            var randomNumberY = random.Next(1, 5);

            if (randomNumberY == 1)
                _positionY = TrackYPosition.OrangeTrack;
            else if (randomNumberY == 2)
                _positionY = TrackYPosition.BlueTrack;
            else if (randomNumberY == 3)
                _positionY = TrackYPosition.YellowTrack;
            else
                _positionY = TrackYPosition.RedTrack;

            Random random2 = new Random();
            var randomNumberX = random2.Next(0, lengthOfTruck);
            _positionX = randomNumberX;


        }

        public class Banana : Bonus
        {
        }
        public class Coin : Bonus
        {
        }
    }
}
