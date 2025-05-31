using APITrip.Entities;
using AutoMapper;
using APITrip.Entities;
using APITrip.models.users;
using APITrip.models.Flotte;
using APITrip.models.Voyages;
using APITrip.models.Chauffeurs;
using APITrip.models.Gestionnaires;
using APITrip.models.Offres;
using APITrip.models.Reservations;
using APITrip.models.Clients;

namespace APITrip.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> User
            CreateMap<CreateRequest, User>();
            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
            (src, dest, prop) =>
            {
                // ignore both null & empty string properties
                if (prop == null) return false;
                if (prop.GetType() == typeof(string) &&
    string.IsNullOrEmpty((string)prop)) return false;
                // ignore null role
                if (x.DestinationMember.Name == "Role" && src.Role ==
    null) return false;
                return true;
            }
            ));
            CreateMap<FlotteCreateRequest, Flotte>();
            CreateMap<VoyageCreateRequest, Voyage>();
            CreateMap<ChauffeurCreateRequest, Chauffeur>();
            CreateMap<GestionnaireCreateRequest, Gestionnaire>();
            CreateMap<OffreCreateRequest, Offre>();
            CreateMap<ReservationCreateRequest, Reservation>();
            CreateMap<ClientCreateRequest, Client>();
        }
    }
}
