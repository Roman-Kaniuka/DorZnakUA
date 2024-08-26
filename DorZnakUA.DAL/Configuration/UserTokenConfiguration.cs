using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.RefreshToken).IsRequired();
        builder.Property(x => x.RefreshTokenExpiryTime).IsRequired();
        
        builder.HasData(new List<UserToken>()
        {
            new UserToken()
            {
                Id = 1,
                RefreshToken = "dfghjkuy",
                RefreshTokenExpiryTime = DateTime.UtcNow,
                UserId = 1,
            }
        });
    }
}