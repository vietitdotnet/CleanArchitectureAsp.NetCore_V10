namespace MyApp.Domain.Core.Models
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; }

        DateTime? DeletedAt { get; }
    }
}