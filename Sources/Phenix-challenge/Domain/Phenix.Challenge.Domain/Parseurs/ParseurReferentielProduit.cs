using Phenix.Challenge.Domain.Exceptions;
using System;

namespace Phenix.Challenge.Domain.Parseurs
{
    public class ParseurReferentielProduit
    {
        public static ReferentielProduit Parse(string ligne, Guid magasin, DateTime date)
        {
            if (string.IsNullOrEmpty(ligne)) throw new ErrorParseException();
            var elements = ligne.Split('|');
            if (elements.Length != 2) throw new ErrorParseException(ligne);

            if (!int.TryParse(elements[0], out int produitId))
                throw new ErrorParseException($"{ligne} - {elements[0]} est invalide.");

            if (!decimal.TryParse(elements[1], out decimal prix))
                throw new ErrorParseException($"{ligne} - {elements[1]} est invalide.");

            return new ReferentielProduit
            {
                ProduitId = produitId,
                Prix = prix,
                Magasin = magasin,
                Date = date
            };
        }
    }
}