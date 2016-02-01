using System;
using System.Diagnostics;
using System.Threading;
using static System.Console;

namespace QuickTimer
{
    class Program
    {
        static readonly string AppName = "QuickTimer 1.0";
        static readonly char ResetChar = 'r';
        static readonly char QuitChar = 'q';
        static readonly string NewLine = Environment.NewLine;


        static void Main(string[] args)
        {
            var timer = new QuickTimer();
            timer.QuitEvent +=  (o, e) => Console.Clear();
            timer.ResetEvent += (o, e) => Console.Clear();
            timer.TickEvent += (o, e) => Console.Write($"\r{timer.ElapsedMilliseconds.ToSeconds()}");
            //Console.WriteLine("Welcome to {0}. {3}Instructions: {3}Press any key to begin or pause. {3}Press '{1}' to reset. {3}Press '{2}' to quit.",
            //    AppName, ResetChar, QuitChar, NewLine);

            //1: Example of new string interpolation
            Console.WriteLine($"Welcome to {AppName}. {NewLine}Instructions: {NewLine}Press any key to begin or pause. {NewLine}Press '{ResetChar}' to reset. {NewLine}Press '{QuitChar} to quit.");
            Console.ReadKey();
            Console.Clear();
            timer.Start();
        }
    }
}
