using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Responses
{
    public interface IIOptionResponse<IOptions> : IResponse
    {
        IEnumerable<IOptions> Model { get; set; }
    }

    public class IOptionResponse<IOptions> : IIOptionResponse<IOptions>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<IOptions> Model { get; set; }
    }
}
