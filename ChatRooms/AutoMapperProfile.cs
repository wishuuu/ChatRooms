using AutoMapper;
using ChatRooms.Domain;
using ChatRooms.Domain.DTOs;

namespace ChatRooms;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<MessageDTO, Message>();
        CreateMap<RoomDTO, Room>();
        CreateMap<UserDTO, User>();
    }
}