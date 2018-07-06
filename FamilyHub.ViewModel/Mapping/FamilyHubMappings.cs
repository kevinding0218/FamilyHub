using AutoMapper;
using FamilyHub.Data.Common;
using FamilyHub.Data.Payment;
using FamilyHub.Data.Transactions;
using FamilyHub.ViewModel.Payment;
using FamilyHub.ViewModel.Transactions;
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

            #region Transaction Category
            this.CreateMap<vmTransactionCategoryCreateRequest, TransactionCategory>()
                .ForMember(target => target.TransactionCategoryID, source => source.Ignore())
                .ForMember(target => target.Timestamp, source => source.Ignore())
                .ForMember(target => target.TransactionDetails, source => source.Ignore());

            this.CreateMap<vmTransactionCategoryUpdateRequest, TransactionCategory>()
                .ForMember(target => target.Timestamp, source => source.Ignore())
                .ForMember(target => target.TransactionDetails, source => source.Ignore());
            #endregion

            #region Transaction
            this.CreateMap<vmTransactionCreateRequest, Transaction>()
                .ForMember(target => target.TransactionID, source => source.Ignore())
                .ForMember(target => target.TransactionDetailID, source => source.Ignore())
                .ForMember(target => target.TransactionDetailFk, source => source.MapFrom(s => new TransactionDetail()
                {
                    PostedDate = s.PostedDate,
                    PaymentMethodID = s.PaymentMethodID,
                    PaymentPayorID = s.PaymentPayorID,
                    TransactionTypeID = s.TransactionTypeID,
                    TransactionCategoryID = s.TransactionCategoryID
                }));

            this.CreateMap<vmTransactionCreateRequest, TransactionDetail>()
                .ForMember(target => target.TransactionDetailID, source => source.Ignore())
                .ForMember(target => target.Timestamp, source => source.Ignore())
                .ForMember(target => target.TransactionTypeFk, source => source.Ignore())
                .ForMember(target => target.TransactionCategoryFk, source => source.Ignore())
                .ForMember(target => target.PaymentMethodFk, source => source.Ignore())
                .ForMember(target => target.PaymentPayorFk, source => source.Ignore())
                .ForMember(target => target.TransactionFk, source => source.Ignore());

            this.CreateMap<vmTransactionUpdateRequest, Transaction>()
                //.ForMember(target => target.TransactionDetailFk, source => source.MapFrom(s => new TransactionDetail()
                //{
                //    TransactionDetailID = s.TransactionDetailID,
                //    PostedDate = s.PostedDate,
                //    PaymentMethodID = s.PaymentMethodID,
                //    PaymentPayorID = s.PaymentPayorID,
                //    TransactionTypeID = s.TransactionTypeID,
                //    TransactionCategoryID = s.TransactionCategoryID
                //}))
                .ForMember(target => target.TransactionDetailFk, source => source.Ignore())
                .AfterMap((source, target) =>
                {
                    var existedTransactionDetail = target.TransactionDetailFk;
                    existedTransactionDetail.PostedDate = source.PostedDate;
                    existedTransactionDetail.PaymentMethodID = source.PaymentMethodID;
                    existedTransactionDetail.PaymentPayorID = source.PaymentPayorID;
                    existedTransactionDetail.TransactionTypeID = source.TransactionTypeID;
                    existedTransactionDetail.TransactionCategoryID = source.TransactionCategoryID;
                });
            #endregion
            #endregion

            #region domain to view
            #region common
            this.CreateMap<User, vmRegisterUserResponse>();
            this.CreateMap<User, vmValidateUserResponse>()
                 .ForMember(target => target.JwtToken, source => source.Ignore())
                 .ForMember(target => target.Password, source => source.MapFrom(s => s.UserPasswords.FirstOrDefault().Password));
            #endregion

            #region Payment
            this.CreateMap<PaymentMethod, vmPaymentMethodListRequest>()
                .ForMember(target => target.PaymentMethodTypeName, source => source.MapFrom(s => s.PaymentMethodTypeFk.PaymentMethodTypeName));
            this.CreateMap<PaymentPayor, vmPaymentPayorListRequest>()
                .ForMember(target => target.PaymentPayorRelationshipName, source => source.MapFrom(s => s.PaymentPayorRelationshipFk.PaymentPayorRelationshipName));
            #endregion
            #region Transaction Category
            this.CreateMap<TransactionCategory, vmTransactionCategoryList>();
            #endregion
            #region Transaction
            this.CreateMap<Transaction, vmTransactionListSimpleRequest>();
            this.CreateMap<Transaction, vmTransactionListFullRequest>()
                .ForMember(target => target.TransactionDetailContent.PostedDate, source => source.MapFrom(s => s.TransactionDetailFk.PostedDate))
                .ForMember(target => target.TransactionDetailContent.TransactionTypeName, source => source.MapFrom(s => s.TransactionDetailFk.TransactionTypeFk.TransactionTypeName))
                .ForMember(target => target.TransactionDetailContent.TransactionCategoryName, source => source.MapFrom(s => s.TransactionDetailFk.TransactionCategoryFk.TransactionCategoryName))
                .ForMember(target => target.TransactionDetailContent.PaymentMethodName, source => source.MapFrom(s => s.TransactionDetailFk.PaymentMethodFk.PaymentMethodName))
                .ForMember(target => target.TransactionDetailContent.PaymentPayorName, source => source.MapFrom(s => s.TransactionDetailFk.PaymentPayorFk.PaymentPayorName));
            //vmTransactionListRequest
            #endregion
            #endregion
        }

    }
}
