using AutoMapper;
using CMS.Core.Dtos;
using CMS.Core.ViewModels;
using CMS.Data.Models;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace CMS.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>().ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));
            CreateMap<CreateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            //CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageUrl, x => x.Ignore());
            ////لانه عملنا دي تي او للكريت بدون الاي دي فبطل الها لازمة
            //    ./*ForMember(x => x.Id, x => x.Ignore());*/
            CreateMap<User, UpdateUserDto>().ForMember(x => x.Image, x => x.Ignore());
        }
    }
}
