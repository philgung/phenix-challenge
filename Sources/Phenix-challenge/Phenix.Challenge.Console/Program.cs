
using Phenix.Challenge.Domain;
using Phenix.Challenge.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Phenix.ChallengeConsole
{
    class Program
    {
        static Action<Action> measures = impl =>
        {
            var sw = new Stopwatch();
            sw.Start();
            impl();
            sw.Stop();
            Console.WriteLine($"Temps écoulé : {sw.ElapsedMilliseconds} ms.");
        };
        static void Main(string[] args)
        {
            var cheminRacine = @"C:\Users\Philippe Desktop\Downloads\PhenixGenerator.1.0.0\PhenixGenerator.0.1\out\20190101";
            var dateDuJour = new DateTime(2019, 01, 01);
            //var cheminRacine = @"../../../../../../data";
            //var dateDuJour = new DateTime(2017, 05, 14);
            LanceGenerationRapport(cheminRacine, dateDuJour);

            // Export rapports en fichier.

            Console.ReadKey();


        }

        private static void LanceGenerationRapport(string cheminRacine, DateTime dateDuJour)
        {
            var rapport_14052017 = new RapportJournalier(dateDuJour, cheminRacine, new LecteurFichier());
            measures(() =>
            {
                var meilleursVentes = rapport_14052017.Obtenir100MeilleursVentesEnGeneral(); // 8 ms
                Console.WriteLine("MeilleursVentes :");
            });

            measures(() =>
            {
                var plusGrosCA = rapport_14052017.Obtenir100PlusGrosChiffreDAffaireEnGeneral(); // 4 ms
                Console.WriteLine("Plus gros chiffre d'affaires :");
            });
        }
    }
}
