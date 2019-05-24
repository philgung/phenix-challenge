using FluentAssertions;
using Moq;
using Phenix.Challenge.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Phenix.Challenge.Domain.Tests
{
    public class RapportJournalierTests
    {
        readonly RapportJournalier rapportJounalier;
        DateTime dateDuJour = new DateTime(2017, 05, 14);
        Guid magasin1;
        public RapportJournalierTests()
        {
            magasin1 = new Guid("2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71");
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

            MockLitReferentielProduitDuJour(lecteurFichierMock,
                new[] {
                    (magasin1, new[] { 5.59M, 1.50M, 0.95M, 11.50M, 37.60M }),
                    (magasin2, new[] { 5.57M, 2.00M, 1.10M, 10.80M, 39.00M }),
                    (magasin3, new[] { 5.54M, 1.50M, 1.00M, 12.00M, 37.90M }),
                    (magasin4, new[] { 5.55M, 2.10M, 1.10M, 11.50M, 37.60M })
                });
            
            rapportJounalier = new RapportJournalier(dateDuJour, string.Empty, lecteurFichierMock.Object);
        }

        [Fact]
        public void Obtenir100MeilleursVentesEnGeneral()
        {
            // Arrange
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

        [Fact]
        public void Obtenir100PlusGrosChiffreDAffaireEnGeneral()
        {
            // Arrange
            // Act
            var plusGrosChiffreDAffaire = rapportJounalier.Obtenir100PlusGrosChiffreDAffaireEnGeneral();
            // Assert
            plusGrosChiffreDAffaire.Should().HaveCount(5);
            plusGrosChiffreDAffaire.ElementAt(0).ProduitId.Should().Be(773);
            plusGrosChiffreDAffaire.ElementAt(0).ChiffreDAffaire.Should().Be(75.20M);
            plusGrosChiffreDAffaire.ElementAt(1).ProduitId.Should().Be(10);
            plusGrosChiffreDAffaire.ElementAt(1).ChiffreDAffaire.Should().Be(69M);
            plusGrosChiffreDAffaire.ElementAt(2).ProduitId.Should().Be(531);
            plusGrosChiffreDAffaire.ElementAt(2).ChiffreDAffaire.Should().Be(50.15M);
            plusGrosChiffreDAffaire.ElementAt(3).ProduitId.Should().Be(39);
            plusGrosChiffreDAffaire.ElementAt(3).ChiffreDAffaire.Should().Be(8M);
            plusGrosChiffreDAffaire.ElementAt(4).ProduitId.Should().Be(55);
            plusGrosChiffreDAffaire.ElementAt(4).ChiffreDAffaire.Should().Be(6M);
        }

        [Fact]
        public void Obtenir100MeilleursVenteParMagasin()
        {
            // Arrange
            // Act
            var meilleursVentesParMagasin = rapportJounalier.Obtenir100MeilleursVentesParMagasin(magasin1);
            // Assert
            meilleursVentesParMagasin.Should().HaveCount(2);
            meilleursVentesParMagasin.ElementAt(0).ProduitId.Should().Be(531);
            meilleursVentesParMagasin.ElementAt(0).QuantiteTotal.Should().Be(5);
            meilleursVentesParMagasin.ElementAt(1).ProduitId.Should().Be(773);
            meilleursVentesParMagasin.ElementAt(1).QuantiteTotal.Should().Be(2);
        }


        [Fact]
        public void Obtenir100PlusGrosChiffreDAffaireParMagasin()
        {
            // Arrange

            // Act
            var plusGrosChiffreDAffaire = rapportJounalier.Obtenir100PlusGrosChiffreDAffaireParMagasin(magasin1);
            // Assert
            plusGrosChiffreDAffaire.Should().HaveCount(2);
            plusGrosChiffreDAffaire.ElementAt(0).ProduitId.Should().Be(773);
            plusGrosChiffreDAffaire.ElementAt(0).ChiffreDAffaire.Should().Be(75.20M);
            plusGrosChiffreDAffaire.ElementAt(1).ProduitId.Should().Be(531);
            plusGrosChiffreDAffaire.ElementAt(1).ChiffreDAffaire.Should().Be(27.95M);
        }

        private void MockLitReferentielProduitDuJour(Mock<ILecteurFichier> lecteurFichierMock,
            (Guid magasin, decimal[] prix)[] referentielMagasin)
        {
            var referentielsDuJour = new Dictionary<Guid, IEnumerable<ReferentielProduit>>();
            foreach (var referentiel in referentielMagasin)
            {
                referentielsDuJour.Add(referentiel.magasin, new List<ReferentielProduit> {
                    CreerReferentielProduit(531, referentiel.prix[0], referentiel.magasin),
                    CreerReferentielProduit(55, referentiel.prix[1], referentiel.magasin),
                    CreerReferentielProduit(39, referentiel.prix[2], referentiel.magasin),
                    CreerReferentielProduit(10, referentiel.prix[3], referentiel.magasin),
                    CreerReferentielProduit(773, referentiel.prix[4], referentiel.magasin),
                });
            }

            lecteurFichierMock.Setup(x => x.LitReferentielsProduitDuJour(It.IsAny<string>(), dateDuJour))
                            .Returns(referentielsDuJour);
        }

        private ReferentielProduit CreerReferentielProduit(int produitId, decimal prix, Guid magasin)
        {
            return new ReferentielProduit { ProduitId = produitId, Prix = prix, Magasin = magasin, Date = dateDuJour };
        }
    }
}
