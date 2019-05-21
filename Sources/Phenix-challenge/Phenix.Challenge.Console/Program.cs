
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
            // création rapport

            // remplissage rapport du dossier data

            // Génération rapports

            // Export rapports en fichier.
            
            Console.WriteLine("Hello World!");


        }
    }
}
