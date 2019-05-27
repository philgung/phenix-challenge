using Phenix.Challenge.Domain;
using Phenix.Challenge.Services;
using System;
using System.Collections.Generic;

namespace Phenix.Challenge.Application
{
    public class RapportService
    {
        private readonly string dossierRacine;
        private readonly ILecteurFichier lecteurFichier;
        // TODO : Cache sur rapport

        public RapportService(string dossierRacine, ILecteurFichier lecteurFichier)
        {
            this.dossierRacine = dossierRacine;
            this.lecteurFichier = lecteurFichier;
        }

        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesJournaliereEnGeneral(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesEnGeneral();
        }
        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireJournaliereEnGeneral(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireEnGeneral();
        }
        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesJournaliereParMagasin(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesParMagasin(magasin);
        }
        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireJournaliereParMagasin(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireParMagasin(magasin);
        }

        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesHebdomadaireEnGeneral(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesEnGeneral();
        }
        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireHebdomadaireEnGeneral(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireEnGeneral();
        }
        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesHebdomadaireParMagasin(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesParMagasin(magasin);
        }
        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireHebdomadaireParMagasin(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireParMagasin(magasin);
        }

        // Concurrent
        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesJournaliereEnGeneralConcurrent(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesEnGeneralConcurrent();
        }

        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireJournaliereEnGeneralConcurrent(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireEnGeneralConcurrent();
        }
        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesJournaliereParMagasinConcurrent(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesParMagasinConcurrent(magasin);
        }
        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireJournaliereParMagasinConcurrent(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour, dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireParMagasinConcurrent(magasin);
        }

        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesHebdomadaireEnGeneralConcurrent(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesEnGeneralConcurrent();
        }
        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireHebdomadaireEnGeneralConcurrent(DateTime dateDuJour)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireEnGeneralConcurrent();
        }
        public IEnumerable<(int ProduitId, int QuantiteTotal)> Obtenir100MeilleursVentesHebdomadaireParMagasinConcurrent(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100MeilleursVentesParMagasinConcurrent(magasin);
        }
        public IEnumerable<(int ProduitId, decimal ChiffreDAffaire)> Obtenir100PlusGrosChiffreDAffaireHebdomadaireParMagasinConcurrent(DateTime dateDuJour, Guid magasin)
        {
            var rapport = new Rapport(dateDuJour.AddDays(-7), dateDuJour, dossierRacine, lecteurFichier);
            return rapport.Obtenir100PlusGrosChiffreDAffaireParMagasinConcurrent(magasin);
        }

    }
}
