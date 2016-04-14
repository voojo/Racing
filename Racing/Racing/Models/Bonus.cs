using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models
{
    public abstract class Bonus
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Length { get; set; }

        private TrackYPosition _positionY;

        public Bonus(int lengthOfTruck, Random random)
        {
            var randomNumberY = random.Next(1, 5);

            if (randomNumberY == 1)
                _positionY = TrackYPosition.OrangeTrack;
            else if (randomNumberY == 2)
                _positionY = TrackYPosition.BlueTrack;
            else if (randomNumberY == 3)
                _positionY = TrackYPosition.YellowTrack;
            else
                _positionY = TrackYPosition.RedTrack;

            var randomNumberX = random.Next(0, lengthOfTruck);
            PositionX = randomNumberX;
            PositionY = (int)_positionY;
        }

        public abstract int GetExtraSpeed();
    }
    public class Banana : Bonus
    {
        public Banana(int lengthOfTruck, Random random)
            : base(lengthOfTruck, random)
        {
            Length = 103;
        }

        public override int GetExtraSpeed()
        {
            return -50;
        }
    }
    public class Coin : Bonus
    {
        public Coin(int lengthOfTruck, Random random)
            : base(lengthOfTruck, random)
        {
            Length = 103;
        }

        public override int GetExtraSpeed()
        {
            return 50;
        }
    }
}
