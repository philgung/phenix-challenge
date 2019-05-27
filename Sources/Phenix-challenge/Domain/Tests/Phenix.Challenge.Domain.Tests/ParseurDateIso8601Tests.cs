using FluentAssertions;
using Phenix.Challenge.Domain.Exceptions;
using Phenix.Challenge.Domain.Parseurs;
using System;
using Xunit;

namespace Phenix.Challenge.Domain.Tests
{
    public class ParseurDateIso8601Tests
    {
        [Theory]
        [InlineData("20170514T223544+0100", 2017, 05, 14, 22, 35, 44)]
        [InlineData("20170414T003551+0100", 2017, 04, 14, 0, 35, 51)]
        [InlineData("20170514T030056+0200", 2017, 05, 14, 3, 0, 56)]
        [InlineData("14/01/2019 03:00:56", 2019, 01, 14, 3, 0, 56)]
        public void ParseDateIso8601(string dateFormatIso8601, int annee, int mois, int jour, 
            int heures, int minutes, int secondes)
        {
            // Arrange
            // Act
            var resultat = ParseurDate.Parse(dateFormatIso8601);
            // Assert
            resultat.Date.Should().Be(new DateTime(annee, mois, jour));
            resultat.Hour.Should().Be(heures);
            resultat.Minute.Should().Be(minutes);
            resultat.Second.Should().Be(secondes);
        }

        [Theory]
        [InlineData("201")]
        [InlineData("20170414T003551+0101")]
        public void ParseDateInvalid_Alors_RetourneFormatException(string dateFormatIso8601)
        {
            // Arrange
            // Act
            Action parseImpl = () => ParseurDate.Parse(dateFormatIso8601);
            // Assert
            parseImpl.Should().Throw<ErrorParseException>();
        }
    }
}
