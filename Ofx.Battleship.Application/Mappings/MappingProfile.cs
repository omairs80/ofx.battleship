using AutoMapper;
using Ofx.Battleship.Application.Commands;
using Ofx.Battleship.Contract.Requests;

namespace Ofx.Battleship.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBattleShipRequest, AddBattleShipCommand>();
            CreateMap<AttackBattleShipRequest, AttackBattleShipCommand>();
        }
    }
}
