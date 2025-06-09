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
            return _context.Gestionnaires;
        }

        public Gestionnaire GetById(int id)
        {
            var gestionnaire = _context.Gestionnaires.Find(id);
            if (gestionnaire == null) throw new KeyNotFoundException("Gestionnaire not found");
            return gestionnaire;
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
            _mapper.Map(model, gestionnaire);
            _context.Gestionnaires.Update(gestionnaire);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var gestionnaire = GetById(id);
            _context.Gestionnaires.Remove(gestionnaire);
            _context.SaveChanges();
        }
    }
}
