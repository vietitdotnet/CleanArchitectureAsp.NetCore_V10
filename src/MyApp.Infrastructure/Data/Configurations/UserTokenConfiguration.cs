using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Data.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<AutUserToken>
{
    public void Configure(EntityTypeBuilder<AutUserToken> builder)
    {
        builder.ToTable("AutUserTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
               .IsRequired();

        builder.Property(x => x.RefreshToken)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.ExpiryTime)
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .IsRequired();

            builder.Property(x => x.IsRevoked)
               .HasDefaultValue(false);

        // Index để tìm refresh token nhanh
        builder.HasIndex(x => x.RefreshToken)
               .IsUnique();

        // Index để query token theo user
        builder.HasIndex(x => x.UserId);

            // builder.HasKey(o => o.Id);

            builder.HasOne(o => (AppUser)o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
}

}
