using System;
using System.Globalization;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class Track : ITrack
    {

        public Track(string data)
        {
            var datas = data.Split(';');
            Tag = datas[0];
            Coordinate = new Coordinate(Convert.ToInt32(datas[1]), Convert.ToInt32(datas[2]), Convert.ToInt32(datas[3]));
            Timestamp = DateTime.ParseExact(datas[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
        }

        public string Tag { get; set; }
        public int Velocity { get; set; }
        public int Heading { get; set; }
        public ICoordinate Coordinate { get; set; }
        public DateTime Timestamp { get; set; }
        

        public void Update(ITrack track)
        {
            var Delta_X = Coordinate.X - track.Coordinate.X;
            var Delta_Y = Coordinate.Y - track.Coordinate.Y;
            var TimeDifference = Timestamp - track.Timestamp;
            var Distance = Math.Sqrt(Math.Pow(Delta_X, 2) + Math.Pow(Delta_Y, 2));
            Velocity = (int) (Distance / TimeDifference.TotalSeconds);


            if (Delta_X >= 0 && Delta_Y >= 0) //Kvadrant 1
            {
                //Heading = 90 - (int)Math.Atan2(Delta_Y, Delta_X)*(180/Math.PI);
            }else if(Delta_X < 0 && Delta_Y >= 0) //Kvadrant 2
            {
                //Heading = 450 - (int)Math.Atan2(Delta_Y, Delta_X)*(180/Math.PI);
            }
            else if(Delta_X < 0 && Delta_Y < 0) //Kvadrant 3
            {
                //Heading =  90 -(int)Math.Atan2(Delta_Y, Delta_X)*(180/Math.PI);
                
            }
            else //Kvadrant 4
            {
                // Heading = 90  -(int)Math.Atan2(Delta_Y, Delta_X)*(180/Math.PI);
            }
            //Heading = (int) (Math.Atan2(Delta_Y, Delta_X));

            //throw new NotImplementedException();
        }
    }
}