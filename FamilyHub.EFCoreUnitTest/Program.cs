using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Repository.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.EFCoreUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                //TestAddNewPaymentPayor();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static string ConnectionString
           => "Server=KD\\SQLEXPRESS; database=SmartFamily; Integrated Security=SSPI;";

        private static async Task TestAddNewPaymentPayor()
        {
            var options = new DbContextOptionsBuilder<FamilyHubDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            using (var _context = new FamilyHubDbContext(options))
            {
                var _repository = new PaymentPayorRepository(_context);

                PaymentPayor newEntity = new PaymentPayor();
                newEntity.PaymentPayorName = "Self";
                newEntity.Active = true;
                newEntity.PaymentSplit = false;
                newEntity.PaymentPayorRelationshipID = 7;
                newEntity.CreatedBy = 1;

                _context.Set<PaymentPayor>().Add(newEntity);
                _context.SaveChanges();

                //await _repository.AddPaymentPayorAsync(newEntity);
            }
        }
    }
}
