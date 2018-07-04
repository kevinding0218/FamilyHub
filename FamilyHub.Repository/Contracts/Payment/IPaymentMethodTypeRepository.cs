using FamilyHub.Data.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Payment
{
    public interface IPaymentMethodTypeRepository : IRepository<PaymentMethodType>
    {
        Task<IEnumerable<PaymentMethodType>> GetListPaymentMethodTypeAsync();
    }
}
