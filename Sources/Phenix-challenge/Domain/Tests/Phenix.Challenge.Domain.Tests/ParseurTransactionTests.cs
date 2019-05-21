using FluentAssertions;
using Phenix.Challenge.Domain.Exceptions;
using Phenix.Challenge.Domain.Parseurs;
using System;

using Xunit;

namespace Phenix.Challenge.Domain.Tests
{
    // Nous avons besoin de déterminer, chaque jour, les 100 produits qui ont les meilleures ventes 
    // et ceux qui génèrent le plus gros Chiffre d'Affaire par magasin et en général.

    public class ParseurTransactionTests
    {
        [Theory]
        [InlineData("1|20170514T223544+0100|2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71|531|5")]
        public void CreerUneTransaction(string ligne)
        {
            // Arrange

            // Act
            var resultat = ParseurTransaction.Parse(ligne);
            // Assert
            resultat.TransactionId.Should().Be(1);
            resultat.Date.Date.Should().Be(new DateTime(2017, 05, 14));
            resultat.Magasin.Should().Be(new Guid("2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71"));
            resultat.ProduitId.Should().Be(531);
            resultat.Quantite.Should().Be(5);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1|20170514T223544+0100|2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71|531")]
        public void CreerUneTransaction_renvoi_ErrorParseException(string ligne)
        {
            // Arrange

            // Act
            Action creerUneTransactionImpl = () => ParseurTransaction.Parse(ligne);
            // Assert
            creerUneTransactionImpl.Should().Throw<ErrorParseException>();
        }
    }
}
