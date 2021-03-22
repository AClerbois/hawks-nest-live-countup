using Sot.Display.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sot.Display.Services
{
    public class CountUpService
    {
        private IList<Domain.CountUpTimer> timers;

        public CountUpService()
        {
            timers = new List<CountUpTimer>
            {
                new CountUpTimer
                {
                    Id = "1",
                    Path = @"d:\Hawks\Kids\Section1\timer.txt",
                    IsStarted = false,
                    Timer = DateTime.Now
                },
                new CountUpTimer
                {
                    Id = "2",
                    Path = @"d:\Hawks\Kids\Section2\timer.txt",
                    IsStarted = false,
                    Timer = DateTime.Now
                }
            };
        }     

        public async Task<bool> ExecuteTickAsync()
        {
            foreach (var timer in timers.Where(t => t.IsStarted))
            {
                await WriteInFileAsync(timer);
            }

            return true;
        }
              
        private async Task WriteInFileAsync(CountUpTimer timer)
        {
            var diff = DateTime.Now.Subtract(timer.Timer);
            await File.WriteAllTextAsync(timer.Path, diff.ToString(@"hh\:mm\:ss"));
        }

        internal void Reset(string id)
        {
            var timer = timers.FirstOrDefault(d => d.Id == id);
            if (timer == null)
                return;

            timer.Timer = DateTime.Now;

            var diff = DateTime.Now.Subtract(timer.Timer);
            File.WriteAllText(timer.Path, diff.ToString(@"hh\:mm\:ss"));
        }

        internal void Start(string id)
        {
            var timer = timers.FirstOrDefault(d => d.Id == id);
            if (timer == null)
                return;

            timer.Timer = DateTime.Now;
            timer.IsStarted = true;
        }

        internal void StartWithCountDown(string id, int secondToCountdown)
        {
            var timer = timers.FirstOrDefault(d => d.Id == id);
            if (timer == null)
                return;

            timer.Timer = DateTime.Now.AddSeconds(secondToCountdown);
            timer.IsStarted = true;
        }

        internal void Stop(string id)
        {
            var timer = timers.FirstOrDefault(d => d.Id == id);
            if (timer == null)
                return;

            timer.IsStarted = false;
        }

    }
}
