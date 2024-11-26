using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class WindZoneCofiguration : IEntityTypeConfiguration<WindZone>
{
    public void Configure(EntityTypeBuilder<WindZone> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);

        builder.HasMany<Project>(x => x.Projects)
            .WithOne(x => x.WindZone)
            .HasForeignKey(x => x.WindZoneId)
            .HasPrincipalKey(x => x.Id);

        builder.HasData(new List<WindZone>()
        {
            new WindZone()
            {
                Id = 1,
                Name = "1 район",
                Description = "опис до району 1"
            },
            
            new WindZone()
            {
                Id = 2,
                Name = "2 район",
                Description = "опис до району 2"
            },
            
            new WindZone()
            {
                Id = 3,
                Name = "3 район",
                Description = "опис до району 3"
            },
            
            new WindZone()
            {
                Id = 4,
                Name = "4 район",
                Description = "опис до району 4"
            },
            
            new WindZone()
            {
                Id = 5,
                Name = "5 район",
                Description = "опис до району 5"
            },
        });
    }
}