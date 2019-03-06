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
            Velocity = 0;
            Heading = 0;
        }

        public string Tag { get; set; }
        public double Velocity { get; set; }
        public int Heading { get; set; }
        public ICoordinate Coordinate { get; set; }
        public DateTime Timestamp { get; set; }
        public void Update(ITrack track)
        {
            var Delta_X = Coordinate.X - track.Coordinate.X;
            var Delta_Y = Coordinate.Y - track.Coordinate.Y;
            var TimeDifference = Timestamp - track.Timestamp;


            var Distance = Math.Sqrt(Math.Pow(Delta_X, 2) + Math.Pow(Delta_Y, 2));
            Velocity = (Distance / TimeDifference.TotalSeconds);


            if (Delta_X >= 0 && Delta_Y >= 0) //Kvadrant 1
            {
                Console.WriteLine("Kvadrant 1: " + Delta_Y + " " + Delta_X);
                Console.WriteLine((int)Math.Atan2(Delta_Y, Delta_X) *(90/57.2957795130823) * (180 / Math.PI));

                Heading = (int)(90 - (int)Math.Atan2(Delta_Y, Delta_X) * (90 / 57.2957795130823) * (180 / Math.PI));
            }else if(Delta_X < 0 && Delta_Y >= 0) //Kvadrant 2
            {
                Console.WriteLine("Kvadrant 2: " + Delta_Y +  " "+ Delta_X);
                Console.WriteLine((int)Math.Atan2(Delta_Y, -Delta_X) * (180 / Math.PI));

                Heading = (int)(270 - (int)Math.Atan2(Delta_Y, Delta_X) * (90 / 57.2957795130823) * (180 / Math.PI));
            }
            else if(Delta_X < 0 && Delta_Y < 0) //Kvadrant 3
            {
                Console.WriteLine("Kvadrant 3: " + Delta_Y + " " + Delta_X);
                Console.WriteLine((int)Math.Atan2(-Delta_Y, Delta_X) * (180 / Math.PI));
                Heading =  (int)(90 -(int)Math.Atan2(Delta_Y, Delta_X) * (90 / 57.2957795130823) * (180 / Math.PI));

            }
            else //Kvadrant 4
            {
                Console.WriteLine("Kvadrant 4: " + Delta_Y + " " + Delta_X);
                Heading = (int)(90  - (int)Math.Atan2(Delta_Y, Delta_X) * (90 / 57.2957795130823) * (180 / Math.PI));
               
            }

        }
    }
}