
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
            // Transaction : 
            // txId|datetime|magasin|produit|qte
            // 1 |20170514T223544+0100|2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71|531|5

            // Referenciel Produit
            // produit|prix
            // 1|4.7

            Console.WriteLine("Hello World!");


        }
    }
}
