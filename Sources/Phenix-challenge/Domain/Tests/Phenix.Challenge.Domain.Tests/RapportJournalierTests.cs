﻿using FluentAssertions;
using Moq;
using Phenix.Challenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Phenix.Challenge.Domain.Tests
{
    public class RapportJournalierTests
    {
        [Fact]
        public void Obtenir100MeilleursVentesEnGeneral()
        {
            // Arrange

            var magasin1 = new Guid("2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71");
            var magasin2 = new Guid("bdc2a431-797d-4b07-9567-67c565a67b84");
            var magasin3 = new Guid("72a2876c-bc8b-4f35-8882-8d661fac2606");
            var magasin4 = new Guid("29366c83-eae9-42d3-a8af-f15339830dc5");
            var transactions = new List<Transaction>
            {
                new Transaction { Magasin = magasin1, ProduitId = 531, Quantite = 5 },
                new Transaction { Magasin = magasin2, ProduitId = 55, Quantite = 3 },
                new Transaction { Magasin = magasin3, ProduitId = 39, Quantite = 8 },
                new Transaction { Magasin = magasin4, ProduitId = 10, Quantite = 6 },
                new Transaction { Magasin = magasin1, ProduitId = 773, Quantite = 2 },
                new Transaction { Magasin = magasin4, ProduitId = 531, Quantite = 4 },
            };

            var lecteurFichierMock = new Mock<ILecteurFichier>();
            lecteurFichierMock.Setup(x => x.LitTransactions(It.IsAny<string>())).Returns(transactions);            
            var rapportJounalier = new RapportJournalier(DateTime.MinValue, string.Empty, lecteurFichierMock.Object);
            // Act
            var meilleursVentes = rapportJounalier.Obtenir100MeilleursVentesEnGeneral();
            // Assert
            meilleursVentes.Should().HaveCount(5);
            meilleursVentes.ElementAt(0).ProduitId.Should().Be(531);
            meilleursVentes.ElementAt(0).QuantiteTotal.Should().Be(9);
            meilleursVentes.ElementAt(1).ProduitId.Should().Be(39);
            meilleursVentes.ElementAt(1).QuantiteTotal.Should().Be(8);
            meilleursVentes.ElementAt(2).ProduitId.Should().Be(10);
            meilleursVentes.ElementAt(2).QuantiteTotal.Should().Be(6);
            meilleursVentes.ElementAt(3).ProduitId.Should().Be(55);
            meilleursVentes.ElementAt(3).QuantiteTotal.Should().Be(3);
            meilleursVentes.ElementAt(4).ProduitId.Should().Be(773);
            meilleursVentes.ElementAt(4).QuantiteTotal.Should().Be(2);
        }
    }
}
