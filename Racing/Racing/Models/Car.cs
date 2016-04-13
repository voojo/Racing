using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models
{
    class Car
    {
        private int _carLength;
        public string Name { get; set; }
        public int ActualCoordinateX { get; set; }
        public int ActualCoordinateY { get; set; }
        public bool IsWinner { get; set; }
        public void Move()
        {
            Random random = new Random();
            ActualCoordinateX += random.Next(1, 5);
        }
        public void MoveToStarLine(int startline)
        {
            ActualCoordinateX = startline;
        }

        public bool IfWin(int finishline)
        {
            IsWinner = ActualCoordinateX >= finishline - _carLength;
            return IsWinner;
        }
        public Car()
        {
            _carLength = 103;
        }



    }


}
