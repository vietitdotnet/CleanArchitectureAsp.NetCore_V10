using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Core.Models
{
    public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity
    where TKey : notnull
    {
        public string CreatedBy { get; set; } = null!;
        public DateTimeOffset CreatedOn { get; set; }
        public string LastModifiedBy { get; set; } = null!;
        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}
