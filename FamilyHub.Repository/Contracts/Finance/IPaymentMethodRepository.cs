using FamilyHub.Data.Finance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Finance
{
    public interface IPaymentMethodRepository : IRepository<PaymentMethod>
    {
        Task<PaymentMethod> GetSinglePaymentMethodByIDAsync(int paymentMethodID);
        Task<IEnumerable<PaymentMethod>> GetListPaymentMethodAsync(int createdBy = 0, int paymentMethodTypeId = 0, bool active = true);
        Task<Int32> AddPaymentMethodAsync(PaymentMethod entity);
        Task<Int32> UpdatePaymentMethodAsync(PaymentMethod entity);
        Task DeactivatePaymentMethodAsync(PaymentMethod entity);
    }
}
