using APITrip.Entities;
using APITrip.models.Gestionnaires;
using APITrip.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace APITrip.Services
{
    public interface IGestionnaireService
    {
       
        IEnumerable<Gestionnaire> GetAll();
        Gestionnaire GetById(int id);
        void Create(GestionnaireCreateRequest model);
        void Update(int id, GestionnaireUpdateRequest model);
        void Delete(int id);
    }
   
    public class GestionnaireService : IGestionnaireService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private static List<Gestionnaire> _gestionnaires = new List<Gestionnaire>();
        private static int _nextId = 1;
        public GestionnaireService(
       DataContext context,
       IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Gestionnaire> GetAll()
        {
            return _gestionnaires;
        }

        public Gestionnaire GetById(int id)
        {
            return _gestionnaires.FirstOrDefault(g => g.Id == id);
        }

        public void Create(GestionnaireCreateRequest model)
        {
            var gestionnaire = _mapper.Map<Gestionnaire>(model);
            gestionnaire.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);
            _context.Gestionnaires.Add(gestionnaire);
            _context.SaveChanges();
        }

        public void Update(int id, GestionnaireUpdateRequest model)
        {
            var gestionnaire = GetById(id);
            if (gestionnaire == null)
                throw new KeyNotFoundException($"Gestionnaire avec l'id {id} non trouvé.");

            // Mettre à jour les propriétés ici
            // Exemples :
            // gestionnaire.Nom = model.Nom;
            // gestionnaire.Prenom = model.Prenom;
            // gestionnaire.Email = model.Email;
        }

        public void Delete(int id)
        {
            var gestionnaire = GetById(id);
            if (gestionnaire == null)
                throw new KeyNotFoundException($"Gestionnaire avec l'id {id} non trouvé.");

            _gestionnaires.Remove(gestionnaire);
        }
    }
}
