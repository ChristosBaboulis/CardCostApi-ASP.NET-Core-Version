using AutoMapper;
using CardCostApplication.Contracts.Responses;
using CardCostApplication.Domain.Entities;

namespace CardCostApplication.Mapping
{
	public class ClearingCostProfile : Profile
	{
		public ClearingCostProfile()
		{
			// Entity -> DTO
			CreateMap<ClearingCost, ClearingCostRepresentation>();

			// DTO -> Entity (ignore Id)
			CreateMap<ClearingCostRepresentation, ClearingCost>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
		}
	}
}
