using Phenix.Challenge.Domain.Exceptions;
using System;

namespace Phenix.Challenge.Domain.Parseurs
{
    public static class ParseurTransaction
    {
        public static Transaction Parse(string ligne)
        {
            if (string.IsNullOrEmpty(ligne)) throw new ErrorParseException();

            var elements = ligne.Split('|');
            if (elements.Length != 5) throw new ErrorParseException(ligne);

            if (!int.TryParse(elements[0], out int transactionId))
                throw new ErrorParseException($"{ligne} - {elements[0]} est invalide.");
            var date = ParseurDate.Parse(elements[1]);
            var magasin = new Guid(elements[2]);
            if (!int.TryParse(elements[3], out int produitId))
                throw new ErrorParseException($"{ligne} - {elements[3]} est invalide.");
            if (!int.TryParse(elements[4], out int quantite))
                throw new ErrorParseException($"{ligne} - {elements[4]} est invalide.");
            return new Transaction
            {
                TransactionId = transactionId,
                Date = date,
                Magasin = magasin,
                ProduitId = produitId,
                Quantite = quantite
            };
        }
    }
}
