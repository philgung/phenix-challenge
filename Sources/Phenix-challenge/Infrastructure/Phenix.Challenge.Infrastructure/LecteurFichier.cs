﻿using Phenix.Challenge.Domain;
using Phenix.Challenge.Domain.Parseurs;
using Phenix.Challenge.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phenix.Challenge.Infrastructure
{
    public class LecteurFichier : ILecteurFichier
    {
        public LecteurFichier()
        {
        }

        public IEnumerable<Transaction> LitTransactions(string cheminFichier)
        {
            return Lit(cheminFichier, ligne => ParseurTransaction.Parse(ligne));
        }

        public IEnumerable<ReferentielProduit> LitReferentielProduit(string cheminFichier)
        {
            return Lit(cheminFichier, ligne =>
            {
                var nomFichier = Path.GetFileName(cheminFichier);
                var guidMagasin = new Guid(nomFichier.Substring(15, 36));
                var date = DateTime.ParseExact(nomFichier.Substring(52, 8), "yyyyMMdd", null);
                return ParseurReferentielProduit.Parse(ligne, guidMagasin, date);
            });
        }

        private IEnumerable<T> Lit<T>(string cheminFichier, Func<string, T> impl)
        {
            // TODO : check fileName    
            var lignes = File.ReadAllLines(cheminFichier);
            return lignes.Select(ligne => impl(ligne));
        }

        public virtual IDictionary<Guid, IEnumerable<ReferentielProduit>> LitReferentielsProduitDUnePeriode(string dossierRacine, DateTime dateDeDebut, DateTime dateDeFin)
        {
            var referentielsParMagasin = new Dictionary<Guid, IEnumerable<ReferentielProduit>>();

            var cheminsfichiersReferentielsParMagasin = new List<string>();
            for (var dateDuJour = dateDeDebut; dateDuJour <= dateDeFin; dateDuJour = dateDuJour.AddDays(1))
            {
                cheminsfichiersReferentielsParMagasin.AddRange(Directory.GetFiles(dossierRacine, $"reference_prod-*_{dateDuJour.ToString("yyyyMMdd")}.data"));
            }

            foreach (var cheminFichierReferentiel in cheminsfichiersReferentielsParMagasin)
            {
                var referentielProduit = LitReferentielProduit(cheminFichierReferentiel);
                referentielsParMagasin.Add(referentielProduit.First().Magasin, referentielProduit);
            }
            return referentielsParMagasin;
        }

        public virtual IEnumerable<Transaction> LitTransactionsDUnePeriode(string dossierRacine, DateTime dateDeDebut, DateTime dateDeFin)
        {
            var transactions = new List<Transaction>();

            for (var dateDuJour = dateDeDebut; dateDuJour <= dateDeFin; dateDuJour = dateDuJour.AddDays(1))
            {
                var nomFichierTransactionDuJour = $"transactions_{dateDuJour.ToString("yyyyMMdd")}.data";
                var transactionsLu = LitTransactions(Path.Combine(dossierRacine, nomFichierTransactionDuJour));
                transactions.AddRange(transactionsLu);
            }           

            return transactions;
        }
    }
}