using System;
using System.Globalization;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Services
{
    public class ConsoleRender : IRender
    {

        public ConsoleRender(AirTrafficMonitor airTrafficMonitor)
        {
            //subscribe to events
            airTrafficMonitor.TrackingsChanged += RenderTrackings;
            airTrafficMonitor.SeparationEventsChanged += RenderSeparationEvents;
        }

        //Writes all tracks in TrackEventArgs to console
        public void RenderTrackings(object sender, TrackEventArgs e)
        {
            Console.WriteLine("Current tracks in the airspace: ");

            foreach (var track in e.Trackings)
            {
                Console.WriteLine(track.ToString());
            }
        }

        //Writes all separationevents in SeparationEventArgs to Console
        public void RenderSeparationEvents(object sender, SeparationEventArgs e)
        {

            Console.WriteLine("Current separationevents in the airspace: ");

            foreach (var separationEvent in e.SeparationEvents)
            {
                string stringToRender = "Tag 1: " + separationEvent.Tag1 + ", Tag 2: " + separationEvent.Tag2 + ", Date: " + 
                    separationEvent.Timestamp.ToString(CultureInfo.InvariantCulture) + " " +separationEvent.Timestamp.Millisecond;

                Console.WriteLine(stringToRender);
            }
        }
    }
}