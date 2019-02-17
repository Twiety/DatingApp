using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Damit AutoMapper die Daten von einer Klasse
            // zu einer anderen transferieren kann,
            // ist es erforderlich, die Source- und Destination-Klasse
            // anzugeben. 
            // Das Mapping erfolgt dann basierend auf den Namen der Properties
            // der beiden Klassen. Eigenschaften, die entweder nur in der einen
            // oder der anderen Klasse existieren, werden nicht übertragen.
            // Abweichend von diesem Standardmapping (conventionbased mapping),
            // kann ein Mapping manuell wie folgt definiert werden.

            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                    // In älteren AutoMapper-Versionen wurde zum Aufruf einer externen Funktion
                    // die Methode ResolveUsing verwendet.
                });

            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });     

            CreateMap<Photo,PhotosForDetailedDto>();
        }
    }
}