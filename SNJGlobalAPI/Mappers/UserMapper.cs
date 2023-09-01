using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;

namespace SNJGlobalAPI.Mappers
{
    public static class UserMapper
    {
        public static MapperConfiguration ActDct =
            new(c => c.CreateProjection<User, UserActDctOutputDto>());

        public static MapperConfiguration AccountConfirm =
            new(c => c.CreateProjection<User, EmailDto>());

        public static MapperConfiguration UserGetForAdmin = new
        (
            c =>
            {
                c.CreateProjection<User, UserGetDtoForAdmin>()
                .ForMember(d => d.CreatedBy, x => x.MapFrom(s => s.creator.FirstName + " " + s.creator.LastName))
                .ForMember(d => d.UpdatedBy, x => x.MapFrom(s => s.updator.FirstName + " " + s.updator.LastName))
                .ForMember(d => d.ActDctBy, x => x.MapFrom(s => s.actDctBy.FirstName + " " + s.actDctBy.LastName))
                .ForMember(d => d.ActDctReason, x => x.MapFrom(s => s.Reason))
                .ForMember(d => d.Roles, x => x.MapFrom(s => s.usersRoles))
                .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
                .ForMember(d => d.Fk_BranchId, x => x.MapFrom(s => s.branch.ID));

                c.CreateProjection<UserRole, RolesGetDto>()
                .ForMember(d => d.RoleId, x => x.MapFrom(s => s.role.Id))
                .ForMember(d => d.RoleName, x => x.MapFrom(s => s.role.Name));
            }
        );



        public static MapperConfiguration UserGetForUser =
            new(c => c.CreateProjection<User, UserGetDtoForUser>());

        public static MapperConfiguration UserPswdReset =
            new(c => c.CreateProjection<User, UserResetDto>());

        public static MapperConfiguration UserLock =
            new(c => c.CreateProjection<User, UserLockDto>());

        public static MapperConfiguration Roles =
            new(c => c.CreateProjection<Role, RolesGetDto>()
                .ForMember(d => d.RoleId, x => x.MapFrom(s => s.Id))
                .ForMember(d => d.RoleName, x => x.MapFrom(s => s.Name)));

        public static MapperConfiguration GetUserForUser =
           new(c => c.CreateProjection<User, UserGetDtoForUser>()
               .ForMember(d => d.Fk_BranchId, x => x.MapFrom(s => s.branch.ID))
               .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name)));

        public static MapperConfiguration GetUserBranch =
           new(c => c.CreateProjection<User, GetUserBranchDto>()
               .ForMember(d => d.Name, x => x.MapFrom(s => s.branch.Name)));

        public static MapperConfiguration General<T, TReturn>() where T : class where TReturn : class =>
            new(c => c.CreateProjection<T, TReturn>());
    }//user mapper class
}//namespace