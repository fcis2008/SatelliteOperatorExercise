using AutoMapper;
using SatelliteOperatorExercise.Model;

namespace SatelliteOperatorExercise.Profiles
{
    public class CollisionProfile : Profile
    {
        public CollisionProfile()
        {
            CreateMap<CollisionEventDTO, CollisionEvent>()
                .ForMember(
                dest => dest.MessageID,
                opt => opt.MapFrom(src => $"{src.MessageID}")
                )
                .ForMember(
                dest => dest.EventID,
                opt => opt.MapFrom(src => $"{src.EventID}")
                )
                .ForMember(
                dest => dest.SatelliteID,
                opt => opt.MapFrom(src => $"{src.SatelliteID}")
                )
                .ForMember(
                dest => dest.CollisionDate,
                opt => opt.MapFrom(src => $"{src.CollisionDate}")
                )
                .ForMember(
                dest => dest.ChaserObjectID,
                opt => opt.MapFrom(src => $"{src.ChaserObjectID}")
                )
                .ForMember(
                dest => dest.ProbabilityOfCollision,
                opt => opt.MapFrom(src => $"{src.ProbabilityOfCollision}")
                )
                .ForMember(
                dest => dest.OperatorID,
                opt => opt.MapFrom(src => $"{src.OperatorID}")
                );

            CreateMap<CollisionEvent, CollisionEventDTO>()
                .ForMember(
                dest => dest.MessageID,
                opt => opt.MapFrom(src => $"{src.MessageID}")
                )
                .ForMember(
                dest => dest.EventID,
                opt => opt.MapFrom(src => $"{src.EventID}")
                )
                .ForMember(
                dest => dest.SatelliteID,
                opt => opt.MapFrom(src => $"{src.SatelliteID}")
                )
                .ForMember(
                dest => dest.CollisionDate,
                opt => opt.MapFrom(src => $"{src.CollisionDate}")
                )
                .ForMember(
                dest => dest.ChaserObjectID,
                opt => opt.MapFrom(src => $"{src.ChaserObjectID}")
                )
                .ForMember(
                dest => dest.ProbabilityOfCollision,
                opt => opt.MapFrom(src => $"{src.ProbabilityOfCollision}")
                )
                .ForMember(
                dest => dest.OperatorID,
                opt => opt.MapFrom(src => $"{src.OperatorID}")
                );
        }
    }
}
