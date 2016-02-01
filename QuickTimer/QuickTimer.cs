using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickTimer
{
    public class QuickTimer
    {
        public event EventHandler TickEvent;
        public event EventHandler PauseEvent;
        public event EventHandler ResetEvent;
        public event EventHandler StartEvent;
        public event EventHandler QuitEvent;

        private bool Paused = false;
        private bool Quit = false;
        private ManualResetEvent _ev = new ManualResetEvent(true);
        private Stopwatch _sw = new Stopwatch();

        public long ElapsedMilliseconds {
            get { return _sw.ElapsedMilliseconds; }
        }


        public void Start()
        {
            OnStart(EventArgs.Empty);
            ThreadPool.QueueUserWorkItem(ReadInput);
            _sw.Start();
            while (true)
            {
                if (Quit) break;
                OnTick(EventArgs.Empty);
                Thread.Sleep(100);
                _ev.WaitOne();
            };
        }

        public virtual void OnStart(EventArgs e)
        {
            var handler = StartEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public void OnPause(EventArgs e)
        {
            var handler = PauseEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public void OnTick(EventArgs e)
        {
            var handler = TickEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }

        public void OnQuit(EventArgs e)
        {
            var handler = QuitEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void OnReset(EventArgs e)
        {
            var handler = ResetEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        void ReadInput(Object stateInfo)
        {
            while (true)
            {
                var input = Console.ReadKey();
                switch (input.KeyChar)
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

        private void TogglePause()
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

        private void ResetTimer()
        {
            _sw.Reset();
            OnReset(EventArgs.Empty);
        }

        private void QuitTimer()
        {
            Quit = true;
            _ev.Set();
            OnQuit(EventArgs.Empty);
        }
    }
}
