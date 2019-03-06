using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class Track : ITrack
    {

        public Track(string data)
        {
            var datas = data.Split(';');
            Tag = datas[0];
            Coordinate = new ThreeDimensionalCoordinate(Convert.ToInt32(datas[1]), Convert.ToInt32(datas[2]), Convert.ToInt32(datas[3]));
            Timestamp = DateTime.ParseExact(datas[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            Velocity = null;
            Heading = null;
        }

        public string Tag { get; set; }
        public double? Velocity { get; set; }
        public int? Heading { get; set; }
        public IThreeDimensionalCoordinate Coordinate { get; set; }
        public DateTime Timestamp { get; set; }
        public void Update(ITrack track)
        {
            var Delta_X = Coordinate.X - track.Coordinate.X;
            var Delta_Y = Coordinate.Y - track.Coordinate.Y;
            var TimeDifference = Timestamp - track.Timestamp;

            CalculateAndSetVelocity(Delta_X,Delta_Y,TimeDifference);
            CalculateAndSetHeading(Delta_X,Delta_Y);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Track);
        }

        protected bool Equals(Track other)
        {
            return string.Equals(Tag, other.Tag);
        }

        public override int GetHashCode()
        {
            return (Tag != null ? Tag.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return "Tag: " + Tag + ", X: " + Coordinate.X + ", Y: " + Coordinate.Y + ", Altitude: " + Coordinate.Z +
                   " " +
                   ", Date: "+ Timestamp.ToString(CultureInfo.InvariantCulture)+ " " + Timestamp.Millisecond + (Velocity!=null ?
                   ", Velocity: " + Velocity : ", Velocity: Unknown") + (Heading != null ?", Heading: " + Heading : ", Heading: Unknown");
        }

        #region Calculations

        private void CalculateAndSetVelocity(int Delta_X, int Delta_Y, System.TimeSpan TimeDifference)
        {
            var distance = Math.Sqrt(Math.Pow(Delta_X, 2) + Math.Pow(Delta_Y, 2));
            Velocity = (distance / TimeDifference.TotalSeconds);
        }

        private void CalculateAndSetHeading(int Delta_X, int Delta_Y)
        {
            var degrees = Math.Atan2(Delta_Y, Delta_X) * (180 / Math.PI);
            if (Delta_X < 0 && Delta_Y >= 0) 
            {
                Heading = (int)(450 - degrees);
            }
            else 
            {
                
                Heading = (int)(90 - degrees);
            }
        }
        #endregion
    }
}