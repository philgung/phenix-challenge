using Phenix.Challenge.Domain;
using Phenix.Challenge.Domain.Parseurs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phenix.Challenge.Infrastructure
{
    public class LecteurFichier
    {
        public LecteurFichier()
        {
        }

        public IEnumerable<Domain.Transaction> LitTransactions(string fileName)
        {
            var lignes = File.ReadAllLines(fileName);
            return lignes.Select(ligne => ParseurTransaction.Parse(ligne));
        }

        public IEnumerable<ReferentielProduit> LitReferentielProduit(string cheminFichier)
        {
            // TODO : check fileName

            var nomFichier = Path.GetFileName(cheminFichier);
            // reference_prod-2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71_20170514.data
            var guidMagasin = new Guid(nomFichier.Substring(15, 36));
            var date = DateTime.ParseExact(nomFichier.Substring(52, 8), "yyyyMMdd", null);


            var lignes = File.ReadAllLines(cheminFichier);
            return lignes.Select(ligne => ParseurReferentielProduit.Parse(ligne, guidMagasin, date));
        }
    }
}