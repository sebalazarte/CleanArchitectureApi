using AutoMapper;
using Domain.DTO;
using Domain.Model;

namespace Domain.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();
        }
    }
}
