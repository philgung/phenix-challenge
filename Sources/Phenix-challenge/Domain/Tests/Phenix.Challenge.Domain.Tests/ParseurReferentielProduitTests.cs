using FluentAssertions;
using Phenix.Challenge.Domain.Exceptions;
using Phenix.Challenge.Domain.Parseurs;
using System;
using Xunit;

namespace Phenix.Challenge.Domain.Tests
{
    public class ParseurReferentielProduitTests
    {
        [Theory]
        [InlineData("1|4.7", 4.7)]
        [InlineData("1|.7", 0.7)]
        [InlineData("1|,7", 0.7)]
        public void ParseReferentielProduit(string ligne, decimal prixAttendu)
        {
            // Arrange
            var date = new DateTime(2017, 05, 14);
            var guidMagasin = new Guid("2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71");
            
            // Act
            var referentielProduit = ParseurReferentielProduit.Parse(ligne, guidMagasin, date);
            // Assert
            referentielProduit.ProduitId.Should().Be(1);
            referentielProduit.Prix.Should().Be(prixAttendu);
            referentielProduit.Date.Should().Be(date);
            referentielProduit.Magasin.Should().Be(guidMagasin);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1|4.7|test")]
        public void ParseReferentielProduit_renvoi_ErrorParseException(string ligne)
        {
            // Arrange

            // Act
            Action implementation = () => ParseurReferentielProduit.Parse(ligne, new Guid("2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71"), new DateTime(2017, 05, 14));

            // Assert
            implementation.Should().Throw<ErrorParseException>();
        }
    }
}
