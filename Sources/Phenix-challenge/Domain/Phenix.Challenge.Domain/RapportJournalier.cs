using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phenix.Challenge.Domain
{
    public class RapportJournalier
    {
        // Transaction du jour
        IEnumerable<Transaction> _transactionDuJour;
        //  Les référentiels par magasin
        Dictionary<Guid, IEnumerable<ReferentielProduit>> _referentielsParMagasin;

        // Nous avons besoin de déterminer, chaque jour, 
        // les 100 produits qui ont les meilleures ventes par magasin 
        public IEnumerable<int> Obtenir100MeilleursVentesParMagasin(Guid magasin)
        {
            throw new NotImplementedException();
        }

        // et ceux qui génèrent le plus gros Chiffre d'Affaire par magasin 

        // les 100 produits qui ont les meilleures ventes en général  
        public IEnumerable<int> Obtenir100MeilleursVentesEnGeneral()
        {
            throw new NotImplementedException();
        }
        // et ceux qui génèrent le plus gros Chiffre d'Affaire en général 


    }
}
