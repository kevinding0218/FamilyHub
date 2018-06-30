using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data
{
    public interface IActivateEntity : IEntity
    {
        Boolean Active { get; set; }
    }
}
