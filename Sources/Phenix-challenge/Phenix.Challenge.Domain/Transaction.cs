using System;

namespace Phenix.Challenge.Domain
{
    public struct Transaction
    {
        public int TransactionId;

        public DateTime Date;

        public Guid Magasin;

        public int ProduitId;
        public int Quantite;        
    }
}