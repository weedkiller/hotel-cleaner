using AutoMapper;
using NawafizApp.Common;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services
{
    public static class DtoMappings
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                // ENTITY TO DTO
                #region ENTITY TO DTO
                cfg.CreateMap<User, IdentityUser>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.UserId));
                ;
                cfg.CreateMap<Language, LanguageDto>();
                cfg.CreateMap<RoomType, RoomTypeDto>();
                cfg.CreateMap<HotelBlock, HotelBlockDto>();
                cfg.CreateMap<FixOrder, FixOrderDto>();
                cfg.CreateMap<FIxOrderEqupment, FixOrderEqupDto>();
                cfg.CreateMap<Room, RoomDto>();
                cfg.CreateMap<RoomStatus, RoomStatusDto>();
                cfg.CreateMap<Equipment, EquipmentDto>();
                cfg.CreateMap<CleanOrder, CleanOrderDto>();
                cfg.CreateMap<User, UserDto>();//.ForMember(des=>des.Role ,opt=>opt.MapFrom(src=>src.Roles.ToList()[0].Name));
               
                //cfg.CreateMap<GuideCity , InputGuideCityDto>();
                //cfg.CreateMap< GuideTown , InputGuideTownDto>();
                // cfg.CreateMap<City, InputCityDto>();

                //cfg.CreateMap<CategoryDescription, CategoryDto>();

                //cfg.CreateMap<Languages2, Languages2Dto>();


                #endregion

                // DTO TO ENTITY
                #region DTO TO ENTTY
                cfg.CreateMap<IdentityUser, User>()
                    .ForMember(dest => dest.UserId,
                        opts => opts.MapFrom(src => src.Id));
                ;
                cfg.CreateMap<LanguageDto, Language>();
                cfg.CreateMap<RoomDto, Room>();
                cfg.CreateMap<FixOrderDto, FixOrder>();
                cfg.CreateMap<FixOrderEqupDto, FIxOrderEqupment>();
                cfg.CreateMap<HotelBlockDto, HotelBlock>();

                cfg.CreateMap<RoomTypeDto, RoomType>();
                cfg.CreateMap<RoomStatusDto, RoomStatus>();
                cfg.CreateMap<EquipmentDto, Equipment>();
                cfg.CreateMap<CleanOrderDto, CleanOrder>();
                //cfg.CreateMap<Languages2Dto, Languages2>();
                #endregion
            });

        }
    }
}
