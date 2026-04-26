using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Core.Models
{
    public abstract class FullAuditableEntity<TKey>
    : AuditableEntity<TKey>, ISoftDelete
    where TKey : notnull
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
