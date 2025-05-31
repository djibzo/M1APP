using APITrip.Entities;
using APITrip.models.Chauffeurs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITrip.Services
{
    public interface IChauffeurService
    {
        IEnumerable<Chauffeur> GetAll();
        Chauffeur GetById(int id);
        void Create(ChauffeurCreateRequest model);
        void Update(int id, ChauffeurUpdateRequest model);
        void Delete(int id);
    }

    public class ChauffeurService : IChauffeurService
    {
        private static List<Chauffeur> _chauffeurs = new List<Chauffeur>();
        private static int _nextId = 1;

        public IEnumerable<Chauffeur> GetAll()
        {
            return _chauffeurs;
        }

        public Chauffeur GetById(int id)
        {
            return _chauffeurs.FirstOrDefault(c => c.Id == id);
        }

        public void Create(ChauffeurCreateRequest model)
        {
            var chauffeur = new Chauffeur
            {
                Id = _nextId++,
                // Copier les propriétés du model vers l'entité Chauffeur
                // Exemple :
                // Nom = model.Nom,
                // Prenom = model.Prenom,
                // Telephone = model.Telephone,
                // etc.
            };

            _chauffeurs.Add(chauffeur);
        }

        public void Update(int id, ChauffeurUpdateRequest model)
        {
            var chauffeur = GetById(id);
            if (chauffeur == null)
                throw new KeyNotFoundException($"Le chauffeur avec l'id {id} n'a pas été trouvé.");

            // Mettre à jour les propriétés du chauffeur avec celles du model
            // Exemple :
            // chauffeur.Nom = model.Nom;
            // chauffeur.Prenom = model.Prenom;
            // chauffeur.Telephone = model.Telephone;
        }

        public void Delete(int id)
        {
            var chauffeur = GetById(id);
            if (chauffeur == null)
                throw new KeyNotFoundException($"Le chauffeur avec l'id {id} n'a pas été trouvé.");

            _chauffeurs.Remove(chauffeur);
        }
    }
}
