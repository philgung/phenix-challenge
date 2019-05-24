using Phenix.Challenge.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phenix.Challenge.Domain
{
    public class RapportJournalier
    {
        // Transaction du jour
        IEnumerable<Transaction> _transactionDuJour;
        //  Les référentiels par magasin
        IDictionary<Guid, IEnumerable<ReferentielProduit>> _referentielsParMagasin;
        private readonly DateTime dateDuJour;
        private readonly string dossierRacine;
        private readonly ILecteurFichier lecteurFichier;

        public RapportJournalier(DateTime dateDuJour, string dossierRacine, ILecteurFichier lecteurFichier)
        {
            this.dateDuJour = dateDuJour;
            this.dossierRacine = dossierRacine;
            this.lecteurFichier = lecteurFichier;
            Initialisation();
        }

        private void Initialisation()
        {
            InitialisationTransactions();
            InitialisationReferentielMagasins();
        }

        private void InitialisationReferentielMagasins()
        {
            _referentielsParMagasin = lecteurFichier.LitReferentielsProduitDuJour(dossierRacine, dateDuJour);
        }

        private void InitialisationTransactions()
        {
            var nomFichierTransactionDuJour = $"transactions_{dateDuJour.ToString("yyyyMMdd")}.data";
            _transactionDuJour = lecteurFichier.LitTransactions(Path.Combine(dossierRacine, nomFichierTransactionDuJour));
        }

        // Nous avons besoin de déterminer, chaque jour, 
        // les 100 produits qui ont les meilleures ventes par magasin 
        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesParMagasin(Guid magasin)
        {
            throw new NotImplementedException();
        }

        // et ceux qui génèrent le plus gros Chiffre d'Affaire par magasin 

        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesEnGeneral()
        {
            var meilleursVente = _transactionDuJour.GroupBy(t => t.ProduitId)
                .Select(g => (ProduitId : g.Key, QuantiteTotal:g.Sum(t => t.Quantite)))
                .OrderByDescending(x => x.QuantiteTotal);

            return meilleursVente.Take(100);
        }

        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireEnGeneral()
        {
            var plusGrosCA = _transactionDuJour.GroupBy(t => t.ProduitId)
                .Select(g => (ProduitId: g.Key, ChiffreAffaire: g.Sum(t => t.Quantite * ObtenirPrixUnitaire(t.ProduitId, t.Magasin))))
                .OrderByDescending(x => x.ChiffreAffaire);
            return plusGrosCA.Take(100);
        }

        private decimal ObtenirPrixUnitaire(int produitId, Guid magasin)
        {
            return _referentielsParMagasin[magasin].Single(r => r.ProduitId == produitId).Prix;
        }
    }
}
