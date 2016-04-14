using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models
{
    class Car
    {
        public int CarLength { get; set; }
        public string Name { get; set; }
        public int ActualCoordinateX { get; set; }
        public int ActualCoordinateY { get; set; }
        public bool IsWinner { get; set; }
        public void Move(Random random)
        {
            ActualCoordinateX += random.Next(1, 10);
        }
        public void MoveToStarLine(int startline)
        {
            ActualCoordinateX = startline;
        }

        public bool IfWin(int finishline)
        {
            IsWinner = ActualCoordinateX >= finishline - CarLength;
            return IsWinner;
        }
        public Car()
        {
            CarLength = 103;
        }



    }


}
