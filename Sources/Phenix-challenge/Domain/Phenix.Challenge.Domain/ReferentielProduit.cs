using System;

namespace Phenix.Challenge.Domain
{
    public struct ReferentielProduit
    {
        public int ProduitId;
        public decimal Prix;

        public Guid Magasin;
        public DateTime Date;
    }
}