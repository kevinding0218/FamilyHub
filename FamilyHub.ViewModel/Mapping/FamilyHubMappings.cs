using AutoMapper;
using FamilyHub.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyHub.ViewModel.Mapping
{

    public class FamilyHubMappings : Profile
    {
        public override string ProfileName => nameof(FamilyHubMappings);

        public FamilyHubMappings()
        {
            #region view to domain
            #region common
            this.CreateMap<vmRegisterUserRequest, User>()
                    .AfterMap((source, target) =>
                    {
                        target.UserPasswords.Add(new UserPassword
                        {
                            Password = source.Password,
                            Active = true,
                            IsTemporary = false,
                            PasswordCreated = DateTime.Now
                        });
                    });
            #endregion
            #endregion

            #region domain to api
            this.CreateMap<User, vmRegisterUserResponse>();
            this.CreateMap<User, vmValidateLoginUserResponse>()
                 .ForMember(target => target.JwtToken, source => source.Ignore())
                 .ForMember(target => target.Password, source => source.MapFrom(s => s.UserPasswords.FirstOrDefault().Password));
            #endregion
        }

    }
}
