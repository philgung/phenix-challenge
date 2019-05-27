using Phenix.Challenge.Domain;
using Phenix.Challenge.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phenix.Challenge.Infrastructure
{
    public class LecteurFichierConcurrent : LecteurFichier
    {
        public override IDictionary<Guid, IEnumerable<ReferentielProduit>> LitReferentielsProduitDUnePeriode(string dossierRacine, DateTime dateDeDebut, DateTime dateDeFin)
        {
            var referentielsParMagasin = new ConcurrentDictionary<Guid, IEnumerable<ReferentielProduit>>();

            var cheminsfichiersReferentielsParMagasin = new List<string>();
            for (var dateDuJour = dateDeDebut; dateDuJour <= dateDeFin; dateDuJour = dateDuJour.AddDays(1))
            {
                cheminsfichiersReferentielsParMagasin.AddRange(Directory.GetFiles(dossierRacine, $"reference_prod-*_{dateDuJour.ToString("yyyyMMdd")}.data"));
            }

            foreach (var cheminFichierReferentiel in cheminsfichiersReferentielsParMagasin)
            {
                var referentielProduit = LitReferentielProduit(cheminFichierReferentiel);
                referentielsParMagasin.TryAdd(referentielProduit.First().Magasin, referentielProduit);
            }
            return referentielsParMagasin;
        }

        public override IEnumerable<Transaction> LitTransactionsDUnePeriode(string dossierRacine, DateTime dateDeDebut, DateTime dateDeFin)
        {
            var transactions = new ConcurrentBag<Transaction>();

            for (var dateDuJour = dateDeDebut; dateDuJour <= dateDeFin; dateDuJour = dateDuJour.AddDays(1))
            {
                var nomFichierTransactionDuJour = $"transactions_{dateDuJour.ToString("yyyyMMdd")}.data";
                var transactionsLu = LitTransactions(Path.Combine(dossierRacine, nomFichierTransactionDuJour));

                foreach (var transactionLu in transactionsLu)
                {
                    transactions.Add(transactionLu);
                }                
            }

            return transactions;
        }
    }
}
