using Rowatch2.Enums;
using System;
using System.Drawing;

namespace Rowatch2.Embeding
{
    public class AutoFeedSetup
    {
        public int MaxFeedValue { get; set; }
        
        public Point LocationFeedButton { get; set; }
        public Point LocationFeedConfirmButton { get; set; }
        public FeedAction Action { get; set; }

        public DateTime LastFeedTime { get; set; }
        public DateTime DelayTime { get; set; }
    }
}