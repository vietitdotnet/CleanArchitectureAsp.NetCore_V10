using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Core.Models
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
