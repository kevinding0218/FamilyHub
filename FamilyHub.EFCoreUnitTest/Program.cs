using FamilyHub.Data.Payment;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Payment;
using FamilyHub.Repository.Repository.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                //setup our DI
                var serviceProvider = new ServiceCollection()
                    .AddDbContext<FamilyHubDbContext>(options => options.UseSqlServer(ConnectionString))
                    .AddSingleton<IPaymentPayorRepository, PaymentPayorRepository>()
                    .BuildServiceProvider();

                var _paymentPayorRepository = serviceProvider.GetService<IPaymentPayorRepository>();
                Console.WriteLine("Hello World!");
                //TestAddNewPaymentPayor(_paymentPayorRepository);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }

        private static string ConnectionString
           => "Server=KD\\SQLEXPRESS; database=SmartFamily; Integrated Security=SSPI;";

        private static async Task TestAddNewPaymentPayor(IPaymentPayorRepository _paymentPayorRepository)
        {
            var options = new DbContextOptionsBuilder<FamilyHubDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            using (var _context = new FamilyHubDbContext(options))
            {
                var _repository = new PaymentPayorRepository(_context);

                PaymentPayor newEntity = new PaymentPayor();
                newEntity.Active = true;
                newEntity.PaymentSplit = false;
                newEntity.PaymentPayorRelationshipID = 7;
                newEntity.CreatedBy = 1;

                await _paymentPayorRepository.AddPaymentPayorAsync(newEntity);
            }
        }
    }
}
