using AutoMapper;

namespace FamilyHub.ViewModel.Mapping
{
    public class FamilyHubAutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<FamilyHubMappings>();
            });
        }
    }
}
