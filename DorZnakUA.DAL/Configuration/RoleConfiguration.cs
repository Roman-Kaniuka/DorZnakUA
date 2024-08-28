using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasData(new List<Role>()
        {
            new Role()
            {
                Id = 1,
                Name = (nameof(Roles.User))
            },
            new Role()
            {
                Id = 2,
                Name = (nameof(Roles.Admin))
            },
            new Role()
            {
                Id = 3,
                Name = (nameof(Roles.Moderator))
            }
        });
    }
}