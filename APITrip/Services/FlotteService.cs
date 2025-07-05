using APITrip.Entities;
using APITrip.Helpers;
using APITrip.models.Flotte;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace APITrip.Services
{
    public interface IFlotteService
    {
        IEnumerable<Flotte> GetAll();
        Flotte GetById(int id);
        void Create(FlotteCreateRequest model);
        void Update(int id, FlotteUpdateRequest model);
        void Delete(int id);
    }

    public class FlotteService : IFlotteService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public FlotteService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Flotte> GetAll()
        {
            return _context.Flottes;
        }

        public Flotte GetById(int id)
        {
            return getFlotte(id);
        }

        public void Create(FlotteCreateRequest model)
        {
            var flotte = new Flotte
            {
                TypeFlotte = model.TypeFlotte,
                MatriculeFlotte = model.MatriculeFlotte
            };
            // Save flotte to database
            _context.Flottes.Add(flotte);
            _context.SaveChanges();
        }

        public void Update(int id, FlotteUpdateRequest model)
        {
            var flotte = getFlotte(id);

            if (model.MatriculeFlotte != flotte.MatriculeFlotte && _context.Flottes.Any(x => x.MatriculeFlotte == model.MatriculeFlotte))
                throw new AppException("Flotte with the matricule '" + model.MatriculeFlotte + "' already exists");

            _mapper.Map(model, flotte);
            _context.Flottes.Update(flotte);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var flotte = getFlotte(id);
            _context.Flottes.Remove(flotte);
            _context.SaveChanges();
        }

        private Flotte getFlotte(int id)
        {
            var flotte = _context.Flottes.Find(id);
            if (flotte == null) throw new KeyNotFoundException("Flotte not found");
            return flotte;
        }
    }
}
