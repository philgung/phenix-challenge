using Phenix.Challenge.Domain;
using System;
using System.Collections.Generic;

namespace Phenix.Challenge.Services
{
    public interface ILecteurFichier
    {
        IEnumerable<Transaction> LitTransactions(string cheminFichier);
        IEnumerable<ReferentielProduit> LitReferentielProduit(string cheminFichier);

        IDictionary<Guid, IEnumerable<ReferentielProduit>> LitReferentielsProduitDUnePeriode(string dossierRacine, DateTime dateDeDebut, DateTime dateDeFin);

        IEnumerable<Transaction> LitTransactionsDUnePeriode(string dossierRacine, DateTime dateDeDebut, DateTime dateDeFin);
    }
}