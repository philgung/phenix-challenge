using FluentAssertions;
using System;
using Xunit;

namespace Phenix.Challenge.Domain.Tests
{
    // Nous avons besoin de déterminer, chaque jour, les 100 produits qui ont les meilleures ventes 
    // et ceux qui génèrent le plus gros Chiffre d'Affaire par magasin et en général.

    // Transaction : 
    // txId|datetime|magasin|produit|qte
    // 1 |20170514T223544+0100|2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71|531|5
    public class ParseurTransactionTests
    {
        [Theory]
        [InlineData("1|20170514T223544+0100|2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71|531|5")]
        public void CreerUneTransaction(string ligne)
        {
            // Arrange

            // Act
            var resultat = Transaction.Parse(ligne);
            // Assert
            resultat.TransactionId.Should().Be(1);

        }
    }
}
