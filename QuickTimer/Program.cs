using System;
using System.Diagnostics;
using System.Threading;
using static System.Console;

namespace QuickTimer
{
    class Program
    {
        static bool Paused = false;
        static bool Quit = false;
        static ManualResetEvent _ev = new ManualResetEvent(true);
        static Stopwatch _sw = new Stopwatch();
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ReadInput);
            _sw.Start();
            while(true) {
                if (Quit) break;
                Write($"\r{_sw.ElapsedMilliseconds.ToSeconds()}");
                Thread.Sleep(100);
                _ev.WaitOne();
            };
            
        }

        static void ReadInput(Object stateInfo)
        {
            while (true)
            {
                var input = Console.ReadKey();
                switch(input.KeyChar)
                {
                    case 'q':
                        QuitTimer();
                        break;
                    case 'r':
                        ResetTimer();
                        TogglePause();
                        break;
                    default:
                        TogglePause();
                        break;
                }
            }
        }

        private static void TogglePause()
        {
            if (!Paused)
            {
                _ev.Reset();
                _sw.Stop();
            }
            else {
                _ev.Set();
                _sw.Start();
            }
            Paused = !Paused;
        }

        private static void ResetTimer()
        {
            _sw.Reset();
            Console.Clear();
        }

        private static void QuitTimer()
        {
            Quit = true;
            _ev.Set();
            Console.Clear();
        }
    }
}
