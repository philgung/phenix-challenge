using Phenix.Challenge.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Phenix.Challenge.Domain
{
    public class Rapport
    {
        IEnumerable<Transaction> _transactions;
        IDictionary<Guid, IEnumerable<ReferentielProduit>> _referentielsParMagasin;
        private readonly DateTime dateDeDebut;
        private readonly DateTime dateDeFin;
        private readonly string dossierRacine;
        private readonly ILecteurFichier lecteurFichier;

        public Rapport(DateTime dateDeDebut, DateTime dateDeFin, string dossierRacine, ILecteurFichier lecteurFichier)
        {
            this.dateDeDebut = dateDeDebut;
            this.dateDeFin = dateDeFin;
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
            _referentielsParMagasin = lecteurFichier.LitReferentielsProduitDUnePeriode(dossierRacine, dateDeDebut, dateDeDebut);
        }

        private void InitialisationTransactions()
        {
            _transactions = lecteurFichier.LitTransactionsDUnePeriode(dossierRacine, dateDeDebut, dateDeDebut);
        }

        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesParMagasin(Guid magasin)
        {
            return Filtrer100MeilleursVentes(_transactions.Where(t => t.Magasin == magasin));
        }

        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesEnGeneral()
        {
            return Filtrer100MeilleursVentes(_transactions);
        }

        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireEnGeneral()
        {
            return Filtrer100PlusGrosChiffreDAffaire(_transactions);
        }        

        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireParMagasin(Guid magasin)
        {
            return Filtrer100PlusGrosChiffreDAffaire(_transactions.Where(t => t.Magasin == magasin));
        }

        private IEnumerable<(int ProduitId, int QuantiteTotal)> Filtrer100MeilleursVentes(IEnumerable<Transaction> transactions)
        {
            var meilleursVente = transactions.GroupBy(t => t.ProduitId)
                .Select(g => (ProduitId: g.Key, QuantiteTotal: g.Sum(t => t.Quantite)))
                .OrderByDescending(x => x.QuantiteTotal);

            return meilleursVente.Take(100);
        }

        private IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Filtrer100PlusGrosChiffreDAffaire(IEnumerable<Transaction> transactions)
        {
            var plusGrosCA = transactions.GroupBy(t => t.ProduitId)
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
