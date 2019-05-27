
using Phenix.Challenge.Application;
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
            LanceGenerationRapport(@"C:\Users\phil_\source\repos\philgung\PhenixGenerator.1.0.0\PhenixGenerator.0.1\out\reference", 
                new DateTime(2019, 01, 14));

            // LanceGenerationRapport(@"../../../../../../data", new DateTime(2017, 05, 14));

            // Export rapports en fichier.

            Console.ReadKey();


        }

        private static void LanceGenerationRapport(string cheminRacine, DateTime dateDuJour)
        {
            var rapportService = new RapportService(cheminRacine, new LecteurFichier());
            //var rapport_journalier = new Rapport(dateDuJour, dateDuJour, cheminRacine, new LecteurFichier());
            measures(() =>
            {
                var meilleursVentes = rapportService.Obtenir100MeilleursVentesJournaliereEnGeneral(dateDuJour); // 8 ms
                
            }, "MeilleursVentes journalieres :");

            measures(() =>
            {
                var plusGrosCA = rapportService.Obtenir100PlusGrosChiffreDAffaireJournaliereEnGeneral(dateDuJour); // 4 ms
            }, "Plus gros chiffre d'affaires journalieres :");

            measures(() =>
            {
                var meilleursVentes = rapportService.Obtenir100MeilleursVentesHebdomadaireEnGeneral(dateDuJour); // 8 ms

            }, "MeilleursVentes hebdo :");

            measures(() =>
            {
                var plusGrosCA = rapportService.Obtenir100PlusGrosChiffreDAffaireHebdomadaireEnGeneral(dateDuJour); // 4 ms
            }, "Plus gros chiffre d'affaires hebdo :");
        }
    }
}
