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
        [InlineData("1|4.7")]             
        public void ParseReferentielProduit(string ligne)
        {
            // Arrange

            // Act
            var referentielProduit = ParseurReferentielProduit.Parse(ligne);
            // Assert
            referentielProduit.ProduitId.Should().Be(1);
            referentielProduit.Prix.Should().Be(4.7M);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1|4.7|test")]
        public void ParseReferentielProduit_renvoi_ErrorParseException(string ligne)
        {
            // Arrange

            // Act
            Action implementation = () => ParseurReferentielProduit.Parse(ligne);

            // Assert
            implementation.Should().Throw<ErrorParseException>();
        }
    }
}
