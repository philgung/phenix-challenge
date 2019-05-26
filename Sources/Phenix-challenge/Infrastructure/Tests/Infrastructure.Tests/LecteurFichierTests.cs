using FluentAssertions;
using Phenix.Challenge.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace Infrastructure.Tests
{
    public class LecteurFichierTests
    {
        const string _dossierRacine = "../../../../../../../../data/";
        DateTime dateDuJour = new DateTime(2017, 05, 14);

        [Fact]
        public void Lit_fichier_transaction_et_retourne_transactions()
        {
            // Arrange
            var lecteurFichier = new LecteurFichier();
            // Act
            var transactions = lecteurFichier.LitTransactions($@"{_dossierRacine}transactions_20170514.data");
            // Assert
            transactions.Should().HaveCount(45906);
        }

        [Theory]
        [InlineData(@"reference_prod-2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71_20170514.data", 999, "2a4b6b81-5aa2-4ad8-8ba9-ae1a006e7d71", "20170514")]
        [InlineData(@"reference_prod-e3d54d00-18be-45e1-b648-41147638bafe_20170514.data", 999, "e3d54d00-18be-45e1-b648-41147638bafe", "20170514")]
        [InlineData(@"reference_prod-6af0502b-ce7a-4a6f-b5d3-516d09514095_20170514.data", 999, "6af0502b-ce7a-4a6f-b5d3-516d09514095", "20170514")]
        [InlineData(@"reference_prod-8e588f2f-d19e-436c-952f-1cdd9f0b12b0_20170514.data", 999, "8e588f2f-d19e-436c-952f-1cdd9f0b12b0", "20170514")]
        [InlineData(@"reference_prod-10f2f3e6-f728-41f3-b079-43b0aa758292_20170514.data", 999, "10f2f3e6-f728-41f3-b079-43b0aa758292", "20170514")]
        [InlineData(@"reference_prod-72a2876c-bc8b-4f35-8882-8d661fac2606_20170514.data", 999, "72a2876c-bc8b-4f35-8882-8d661fac2606", "20170514")]
        [InlineData(@"reference_prod-29366c83-eae9-42d3-a8af-f15339830dc5_20170514.data", 999, "29366c83-eae9-42d3-a8af-f15339830dc5", "20170514")]
        [InlineData(@"reference_prod-af068240-8198-4b79-9cf9-c28c0db65f63_20170514.data", 999, "af068240-8198-4b79-9cf9-c28c0db65f63", "20170514")]
        [InlineData(@"reference_prod-bdc2a431-797d-4b07-9567-67c565a67b84_20170514.data", 999, "bdc2a431-797d-4b07-9567-67c565a67b84", "20170514")]
        [InlineData(@"reference_prod-bf0999da-ae45-49df-983e-89020198330b_20170514.data", 999, "bf0999da-ae45-49df-983e-89020198330b", "20170514")]
        [InlineData(@"reference_prod-d4bfbabf-0160-4e87-8688-78e0943a396a_20170514.data", 999, "d4bfbabf-0160-4e87-8688-78e0943a396a", "20170514")]
        [InlineData(@"reference_prod-dd43720c-be43-41b6-bc4a-ac4beabd0d9b_20170514.data", 999, "dd43720c-be43-41b6-bc4a-ac4beabd0d9b", "20170514")]

        public void Lit_fichier_produit_referentiel_et_retourne_referentiels(string fileName, int nbReferentielsAttendus, string guidMagasin, string date)
        {
            // Arrange
            var lecteurFichier = new LecteurFichier();
            // Act
            var referentiels = lecteurFichier.LitReferentielProduit($"{_dossierRacine}{fileName}");
            // Assert
            referentiels.Should().HaveCount(nbReferentielsAttendus);
            referentiels.First().Magasin.Should().Be(new Guid(guidMagasin));
            referentiels.First().Date.ToString("yyyyMMdd").Should().Be(date);
        }

        [Fact]
        public void Lit_referentiels_produit_du_jour()
        {
            // Arrange
            var lecteurFichier = new LecteurFichier();
            // Act
            var referentielsProduitDuJour = lecteurFichier.LitReferentielsProduitDUnePeriode(_dossierRacine, dateDuJour, dateDuJour);
            // Assert
            referentielsProduitDuJour.Should().HaveCount(12);
        }
    }
}
