using AutoMapper;
using FamilyHub.API.ViewModel;
using FamilyHub.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<vmRegisterUser, User>()
                    //.ForMember(target => target.UserID, source => source.Ignore())
                    //.ForMember(target => target.MiddleInitial, source => source.Ignore())
                    //.ForMember(target => target.Active, source => source.Ignore())
                    //.ForMember(target => target.IsCoreUser, source => source.Ignore())
                    //.ForMember(target => target.LastLogin, source => source.Ignore())
                    //.ForMember(target => target.Note, source => source.Ignore())
                    //.ForMember(target => target.RefreshToken, source => source.Ignore())
                    //.ForMember(target => target.CreatedBy, source => source.Ignore())
                    //.ForMember(target => target.CreatedOn, source => source.Ignore())
                    //.ForMember(target => target.LastUpdatedBy, source => source.Ignore())
                    //.ForMember(target => target.LastUpdatedOn, source => source.Ignore())
                    //.ForMember(target => target.ContactAddressID, source => source.Ignore())
                    //.ForMember(target => target.ContactAddressFk, source => source.Ignore())
                    //.ForMember(target => target.UserPasswords, source => source.Ignore())
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
        }

    }
}
