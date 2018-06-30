using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data
{
    public interface IAuditableEntity : IEntity
    {
        int? CreatedBy { get; set; }

        DateTime? CreatedOn { get; set; }

        int? LastUpdatedBy { get; set; }

        DateTime? LastUpdatedOn { get; set; }
    }
}
