using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models
{
    class Track
    {
        public int FinishLineCoordinateX { get; set; }
        public int StartLineCoordinateX { get; set; }
    }
    public enum TrackYPosition
    {
        OrangeTrack = 50,
        BlueTrack = 100,
        YellowTrack = 150,
        RedTrack = 200,
    }

}
