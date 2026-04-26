namespace MyApp.Domain.Core.Models
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedOn { get; set; }
        string? DeletedBy { get; set; }
    }
}