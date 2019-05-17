
using System;
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
            

            // Referenciel Produit
            // produit|prix
            // 1|4.7

            Console.WriteLine("Hello World!");


        }
    }
}
