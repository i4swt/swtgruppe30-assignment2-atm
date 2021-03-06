﻿using System;
using AirTrafficMonitor.Lib.Interfaces;

namespace AirTrafficMonitor.Lib.Models
{
    public class SeparationEvent : ISeparationEvent
    {
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public DateTime Timestamp { get; set; }

        public SeparationEvent(string tag1, string tag2, DateTime timestamp)
        {
            if (!IsTagsValid(tag1, tag2))
            {
                throw new ArgumentException("A tag must have a length of 6 characters");
            }

            Tag1 = tag1;
            Tag2 = tag2;
            Timestamp = timestamp;
        }

        private bool IsTagsValid(string tag1, string tag2)
        {
            return 
                tag1 != null && 
                tag2 != null && 
                tag1.Length == 6 && 
                tag2.Length == 6 && //length of 6 is defined as a requirement in the assignment
                tag1 != tag2; //A plane cannot be in a separation event with itself
        }

        public override string ToString()
        {
            return Tag1 + " <---> " + Tag2 + " @ " + Timestamp.ToString("dd-MM-yyyy HH:mm:ss");
        }
    }
}
