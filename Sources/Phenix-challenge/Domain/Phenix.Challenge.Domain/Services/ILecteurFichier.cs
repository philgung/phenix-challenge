using Phenix.Challenge.Domain;
using System.Collections.Generic;

namespace Phenix.Challenge.Services
{
    public interface ILecteurFichier
    {
        IEnumerable<Transaction> LitTransactions(string cheminFichier);
        IEnumerable<ReferentielProduit> LitReferentielProduit(string cheminFichier);
    }
}