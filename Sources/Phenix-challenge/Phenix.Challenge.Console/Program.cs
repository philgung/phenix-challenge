
using Phenix.Challenge.Domain;
using Phenix.Challenge.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Phenix.ChallengeConsole
{
    class Program
    {
        static Action<Action, string> measures = (impl, label) =>
        {
            Console.WriteLine(label);
            var sw = new Stopwatch();
            sw.Start();
            impl();
            sw.Stop();
            Console.WriteLine($"Temps écoulé : {sw.ElapsedMilliseconds} ms.");
        };
        static void Main(string[] args)
        {
            LanceGenerationRapport(@"C:\Users\Philippe Desktop\Downloads\PhenixGenerator.1.0.0\PhenixGenerator.0.1\out\20190101", 
                new DateTime(2019, 01, 01));

            // LanceGenerationRapport(@"../../../../../../data", new DateTime(2017, 05, 14));

            // Export rapports en fichier.

            Console.ReadKey();


        }

        private static void LanceGenerationRapport(string cheminRacine, DateTime dateDuJour)
        {
            var rapport_journalier = new Rapport(dateDuJour, dateDuJour, cheminRacine, new LecteurFichier());
            measures(() =>
            {
                var meilleursVentes = rapport_journalier.Obtenir100MeilleursVentesEnGeneral(); // 8 ms
                
            }, "MeilleursVentes :");

            measures(() =>
            {
                var plusGrosCA = rapport_journalier.Obtenir100PlusGrosChiffreDAffaireEnGeneral(); // 4 ms
            }, "Plus gros chiffre d'affaires :");
        }
    }
}
