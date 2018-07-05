using AutoMapper;
using FamilyHub.Data.Common;
using FamilyHub.Data.Payment;
using FamilyHub.ViewModel.Payment;
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

            #region Payment
            this.CreateMap<vmPaymentMethodCreateRequest, PaymentMethod>()
                .ForMember(target => target.PaymentMethodID, source => source.Ignore())
                .ForMember(target => target.CreatedBy, source => source.Ignore())
                .ForMember(target => target.CreatedOn, source => source.Ignore())
                .ForMember(target => target.LastUpdatedBy, source => source.Ignore())
                .ForMember(target => target.LastUpdatedOn, source => source.Ignore())
                .ForMember(target => target.Timestamp, source => source.Ignore())
                .ForMember(target => target.PaymentMethodTypeFk, source => source.Ignore())
                .ForMember(target => target.TransactionDetails, source => source.Ignore());

            this.CreateMap<vmPaymentMethodUpdateRequest, PaymentMethod>()
                .ForMember(target => target.PaymentMethodID, source => source.Ignore())
                .ForMember(target => target.CreatedBy, source => source.Ignore())
                .ForMember(target => target.CreatedOn, source => source.Ignore())
                .ForMember(target => target.LastUpdatedBy, source => source.Ignore())
                .ForMember(target => target.LastUpdatedOn, source => source.Ignore())
                .ForMember(target => target.Timestamp, source => source.Ignore())
                .ForMember(target => target.PaymentMethodTypeFk, source => source.Ignore())
                .ForMember(target => target.TransactionDetails, source => source.Ignore());

            this.CreateMap<vmPaymentPayorCreateRequest, PaymentPayor>();
            this.CreateMap<vmPaymentPayorUpdateRequest, PaymentPayor>();
            #endregion
            #endregion

            #region domain to view
            this.CreateMap<User, vmRegisterUserResponse>();
            this.CreateMap<User, vmValidateUserResponse>()
                 .ForMember(target => target.JwtToken, source => source.Ignore())
                 .ForMember(target => target.Password, source => source.MapFrom(s => s.UserPasswords.FirstOrDefault().Password));
            #endregion
        }

    }
}
