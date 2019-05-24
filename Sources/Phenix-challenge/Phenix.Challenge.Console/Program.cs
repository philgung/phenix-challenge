
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
            // création rapport
            var rapport_14052017 = new RapportJournalier(new DateTime(2017, 05, 14), "../../../../../../data", new LecteurFichier());
            // remplissage rapport du dossier data

            // Génération rapports
            measures(() => {
                var meilleursVentes = rapport_14052017.Obtenir100MeilleursVentesEnGeneral();
                Console.WriteLine("MeilleursVentes :");
            });

            measures(() =>
            {
                var plusGrosCA = rapport_14052017.Obtenir100PlusGrosChiffreDAffaireEnGeneral();
                Console.WriteLine("Plus gros chiffre d'affaires :");
            });



            // Export rapports en fichier.

            Console.ReadKey();


        }
    }
}
