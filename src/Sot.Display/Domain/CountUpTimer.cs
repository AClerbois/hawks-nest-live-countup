using System;

namespace Sot.Display.Domain
{
    public class CountUpTimer
    {
        public string Path { get; set; }
        public string Id { get; set; }
        public bool IsStarted { get; set; }
        public DateTime Timer { get; set; }
    }
}
