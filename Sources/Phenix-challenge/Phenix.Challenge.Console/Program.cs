
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Phenix.ChallengeConsole
{
    class Program
    {
        Action<Action> measures = impl =>
        {
            var sw = new Stopwatch();
            sw.Start();
            impl();
            sw.Stop();
            Console.WriteLine($"Temps écoulé : {sw.ElapsedMilliseconds} ms.");
        };
        static void Main(string[] args)
        {

            // Parseurs !
            // Infrastructure -> array[]
            // array[] -> filtre -> parseur -> Object !

            
            Console.WriteLine("Hello World!");


        }
    }
}
