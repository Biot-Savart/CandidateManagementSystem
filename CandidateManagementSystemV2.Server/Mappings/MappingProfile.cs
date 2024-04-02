using AutoMapper;
using CandidateManagementSystemV2.Server.DTOs;
using CandidateManagementSystemV2.Server.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CandidateManagementSystemV2.Server.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define mappings here
            CreateMap<CandidateDto, Candidate>();
            CreateMap<Candidate, CandidateDto>();
            CreateMap<SkillDto, Skill>();
            CreateMap<Skill, SkillDto>();
            CreateMap<CandidatePositionDto, CandidatePosition>();
            CreateMap<CandidatePosition, CandidatePositionDto>();
            CreateMap<PositionDto, Position>();
            CreateMap<Position, PositionDto>();
            // Add other mappings as needed
        }
    }
}
