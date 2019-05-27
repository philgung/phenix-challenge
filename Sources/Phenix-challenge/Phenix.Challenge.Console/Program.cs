
using Phenix.Challenge.Application;
using Phenix.Challenge.Infrastructure;
using System;
using System.Diagnostics;
using System.Linq;

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
                new DateTime(2019, 01, 1));
            LanceGenerationRapportConcurrent(@"C:\Users\phil_\source\repos\philgung\PhenixGenerator.1.0.0\PhenixGenerator.0.1\out\reference",
                new DateTime(2019, 01, 8));
            // Export rapports en fichier.

            Console.ReadKey();


        }

        private static void LanceGenerationRapportConcurrent(string cheminRacine, DateTime dateDuJour)
        {
            var rapportServiceConcurrent = new RapportService(cheminRacine, new LecteurFichierConcurrent());

            measures(() =>
            {
                var meilleursVentes = rapportServiceConcurrent.Obtenir100MeilleursVentesJournaliereEnGeneralConcurrent(dateDuJour);
            }, "Meilleurs Ventes journalieres Concurrente :");

            measures(() =>
            {
                var plusGrosCA = rapportServiceConcurrent.Obtenir100PlusGrosChiffreDAffaireJournaliereEnGeneralConcurrent(dateDuJour);
            }, "Plus gros chiffre d'affaires journalieres Concurrente :");

            measures(() =>
            {
                var meilleursVentes = rapportServiceConcurrent.Obtenir100MeilleursVentesHebdomadaireEnGeneralConcurrent(dateDuJour);
            }, "Meilleurs Ventes hebdo Concurrente :");

            measures(() =>
            {
                var plusGrosCA = rapportServiceConcurrent.Obtenir100PlusGrosChiffreDAffaireHebdomadaireEnGeneralConcurrent(dateDuJour);
            }, "Plus gros chiffre d'affaires hebdo Concurrente :");
        }

        private static void LanceGenerationRapport(string cheminRacine, DateTime dateDuJour)
        {
            var rapportService = new RapportService(cheminRacine, new LecteurFichier());
            measures(() =>
            {
                var meilleursVentes = rapportService.Obtenir100MeilleursVentesJournaliereEnGeneral(dateDuJour);
            }, "Meilleurs Ventes journalieres :");


            measures(() =>
            {
                var plusGrosCA = rapportService.Obtenir100PlusGrosChiffreDAffaireJournaliereEnGeneral(dateDuJour);
            }, "Plus gros chiffre d'affaires journalieres :");

            measures(() =>
            {
                var meilleursVentes = rapportService.Obtenir100MeilleursVentesHebdomadaireEnGeneral(dateDuJour);
            }, "Meilleurs Ventes hebdo :");

            measures(() =>
            {
                var plusGrosCA = rapportService.Obtenir100PlusGrosChiffreDAffaireHebdomadaireEnGeneral(dateDuJour);
            }, "Plus gros chiffre d'affaires hebdo :");

        }

        private static void GenererFichier()
        {
            //Les résultats sont les fichiers:

            //top_100_ventes_<ID_MAGASIN> _YYYYMMDD.data
            //top_100_ventes_GLOBAL_YYYYMMDD.data
            //top_100_ca_<ID_MAGASIN> _YYYYMMDD.data
            //top_100_ca_GLOBAL_YYYYMMDD.data
            //top_100_ventes_<ID_MAGASIN> _YYYYMMDD-J7.data
            //top_100_ventes_GLOBAL_YYYYMMDD - J7.data
            //top_100_ca_<ID_MAGASIN> _YYYYMMDD-J7.data
            //top_100_ca_GLOBAL_YYYYMMDD - J7.data


        }



    }
}
